using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // If your client secret is base64 encoded, then uncomment these two lines
            //var keyAsBase64 = Configuration["auth0:clientSecret"].Replace('_', '/').Replace('-', '+');
            //var secret = Convert.FromBase64String(keyAsBase64);

            // If your client secret is base64 encoded, then comment out this line, and rather use 2 lines above
            var secret = Encoding.UTF8.GetBytes(Configuration["auth0:clientSecret"]);

            var options = new JwtBearerOptions
            {
                TokenValidationParameters =
                {
                    ValidIssuer = $"https://{Configuration["auth0:domain"]}/",
                    ValidAudience = Configuration["auth0:clientId"],
                    IssuerSigningKey = new SymmetricSecurityKey(secret)                
                }
            };
            app.UseJwtBearerAuthentication(options);

            app.UseMvc();
        }
    }
}
