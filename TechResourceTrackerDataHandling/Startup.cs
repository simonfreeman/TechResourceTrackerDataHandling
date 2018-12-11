using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TechResourceTrackerDataHandling.Models;
using Microsoft.EntityFrameworkCore;
using TechResourceTrackerDataHandling.Middleware;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace TechResourceTrackerDataHandling
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
            services.AddDbContext<TechResourcesContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TechResourcesDatabase")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //why would csp want to send parseable json when it could send a brand new media type with 
            //properties that require newtonsoft to map to c# models!?
            services.Configure<MvcOptions>(options =>
            {
                options.InputFormatters
                    .OfType<JsonInputFormatter>()
                    .First(formatter => formatter.SupportedMediaTypes.Contains("application/json"))
                    .SupportedMediaTypes.Add("application/csp-report");
            });
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(builder =>
                    builder.WithOrigins("http://localhost:8080"));
            }
            else
            {
                app.UseHsts();
            }

            app.UseContentSecurityPolicy();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
