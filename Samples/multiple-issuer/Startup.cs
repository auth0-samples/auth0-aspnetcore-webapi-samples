using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MultipleIssuer
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
            // If accessing this API from a browser you'll need to add a CORS policy, see https://docs.microsoft.com/en-us/aspnet/core/security/cors

            services.AddAuthentication()
                .AddJwtBearer("Auth0DomainOne", options =>
                {
                    options.Authority = $"https://{Configuration["Auth0:DomainOne"]}";
                    options.Audience = Configuration["Auth0:Audience"];

                })
                .AddJwtBearer("Auth0DomainTwo", options =>
                {
                    options.Authority = $"https://{Configuration["Auth0:DomainTwo"]}";
                    options.Audience = Configuration["Auth0:Audience"];
                });

            services
                .AddAuthorization(options =>
                {
                    options.DefaultPolicy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .AddAuthenticationSchemes("Auth0DomainOne", "Auth0DomainTwo")
                        .Build();
                });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
