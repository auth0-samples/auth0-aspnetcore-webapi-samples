using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace WebAPIApplication
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:messages",
                    policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", domain)));
                options.AddPolicy("create:messages",
                    policy => policy.Requirements.Add(new HasScopeRequirement("create:messages", domain)));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var options = new JwtBearerOptions
            {
                TokenValidationParameters =
                {
                    ValidIssuer = $"https://{Configuration["Auth0:Domain"]}/",
                    ValidAudience = Configuration["Auth0:ApiIdentifier"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth0:ApiSecret"]))
                }
            };
            app.UseJwtBearerAuthentication(options);

            app.UseMvc();
        }
    }
}
