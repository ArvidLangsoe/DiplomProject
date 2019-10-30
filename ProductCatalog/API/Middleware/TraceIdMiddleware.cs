using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Middleware
{
    public class TraceIdMiddleware
    {
        private readonly RequestDelegate _next;


        public TraceIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            string traceId = context.Request.Headers["TraceId"];

            if (String.IsNullOrWhiteSpace(traceId)) {

                context.Request.Headers["TraceId"] = Guid.NewGuid().ToString();
            }

            await _next.Invoke(context);
        }

    }

    public static class TraceIdExtension {
        public static IApplicationBuilder UseTraceId(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TraceIdMiddleware>();
        }
    }
}
