using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechResourceTrackerDataHandling.Middleware
{
    public static class ContentSecurityPolicyMiddlewareExtensions
    {
        public static IApplicationBuilder UseContentSecurityPolicy(
           this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ContentSecurityPolicyMiddleware>();
        }
    }
}
