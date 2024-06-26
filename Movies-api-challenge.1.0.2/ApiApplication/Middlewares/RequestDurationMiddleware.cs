﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ApiApplication.Middlewares
{
    public class RequestDurationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestDurationMiddleware> _logger;

        public RequestDurationMiddleware(RequestDelegate next, ILogger<RequestDurationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            await _next.Invoke(context);
            watch.Stop();

            var service = context.Request.Path.Value;
            _logger.LogInformation($"----> {service} took {watch.ElapsedMilliseconds} milliseconds");
        }
    }
}
