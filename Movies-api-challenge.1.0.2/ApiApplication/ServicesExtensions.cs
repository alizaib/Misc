using ApiApplication.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ProtoDefinitions;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static ApiApplication.Constants;

namespace ApiApplication
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration configuration) {            
            services.AddSwaggerGen(c => {
                var apiKey = configuration.GetValue<string>(ConfigKeys.Api);

                c.SwaggerDoc("v1", new OpenApiInfo() { 
                    Description = $"The Api Key you need is {apiKey}"
                });

                var securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = ApiKey.Scheme
                    },
                    Name = Headers.ApiKey,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = $"Authorization by {Headers.ApiKey} inside request's header",
                    Scheme = ApiKey.Scheme
                };

                c.AddSecurityDefinition(ApiKey.Scheme, securityScheme);

                var requirement = new OpenApiSecurityRequirement {
                    { securityScheme, new List<string>() }
             };
                c.AddSecurityRequirement(requirement);
            });
            return services;
        }

        public static IServiceCollection ConfgureGrpcClient(this IServiceCollection services, IConfiguration configuration) {

            services.AddGrpcClient<MoviesApi.MoviesApiClient>(o => {
                var url = configuration.GetValue<string>(ConfigKeys.ApiUrl);
                o.Address = new System.Uri(url);
            })
            .ConfigurePrimaryHttpMessageHandler(() => {
                var httpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
            return httpHandler;
            })
            .AddCallCredentials((context, metadata, sp) => {
                var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                var request = httpContextAccessor.HttpContext.Request;
                var apiKey = request.Headers[Headers.ApiKey].FirstOrDefault();

                metadata.Add(Headers.ApiKey, apiKey);
                return Task.CompletedTask;
            });

            return services;
        }
    }
}
