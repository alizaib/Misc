using ApiApplication.Auth;
using ApiApplication.Database;
using ApiApplication.Database.Repositories;
using ApiApplication.Database.Repositories.Abstractions;
using ApiApplication.Middlewares;
using ApiApplication.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using static ApiApplication.Constants;

namespace ApiApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddHttpContextAccessor();
            services.AddScoped<ApiClientGrpc>();
            services.ConfigureSwagger(Configuration);

            services.AddAuthentication(ApiKey.Scheme)
                .AddScheme<ApiKeyAuthSchemeOptions, ApiKeyAuthSchemeHandler>(ApiKey.Scheme,
                    opt => opt.ApiKey = Configuration.GetValue<string>(ConfigKeys.Api));

            services.AddTransient<IShowtimesRepository, ShowtimesRepository>();
            services.AddTransient<ITicketsRepository, TicketsRepository>();
            services.AddTransient<IAuditoriumsRepository, AuditoriumsRepository>();
            services.AddScoped<ITicketExpiryPolicy, TicketExpiryPolicy>();
            //services.AddScoped<ISystemClock, SystemClock>();

            services.AddDbContext<CinemaContext>(options =>
            {
                options.UseInMemoryDatabase("CinemaDb")
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
            services.AddControllers();
            services.AddScoped(_ => {
                var redisConnectionString = Configuration.GetValue<string>(ConfigKeys.RedisUrl);
                var configuration = ConfigurationOptions.Parse(redisConnectionString);
                var redisConnection = ConnectionMultiplexer.Connect(configuration);
                var redisCache = redisConnection.GetDatabase();
                return redisCache;
            });
            


            services.ConfgureGrpcClient(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<RequestDurationMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SampleData.Initialize(app);
        }
    }
}
