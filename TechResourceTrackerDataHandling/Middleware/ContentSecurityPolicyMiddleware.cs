using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechResourceTrackerDataHandling.Middleware
{
    public class ContentSecurityPolicyMiddleware
    {
        private readonly RequestDelegate _next;

        public ContentSecurityPolicyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("Content-Security-Policy", @"default-src  https://*; report-uri /api/CspReports");
            await _next(context);
        }
    }
}
