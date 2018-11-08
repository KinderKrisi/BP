using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebAPI.Authorization;
using Services.IdentityServer;
using IdentityServer4.AccessTokenValidation;
using WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Repositories.Interfaces;
using Repositories;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private OwinConfiguration _owinConfiguration;
        private IdentityConfiguration _identityConfiguration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _owinConfiguration = new OwinConfiguration();
            Configuration.GetSection("OwinConfiguration").Bind(_owinConfiguration);
            _identityConfiguration = new IdentityConfiguration();
            Configuration.GetSection("IdentityConfiguration").Bind(_identityConfiguration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("https://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            });

            services.AddDbContext<BefordingTestContext>(opt =>
                opt.UseSqlServer("Data Source=localhost;Initial Catalog=BefordingTestDb;Integrated Security=True;"));

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                 .AddIdentityServerAuthentication(options =>
    {
        options.Authority = _identityConfiguration.Authority;
        options.ApiName = _identityConfiguration.ApiName;
    });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserMustBeAdministrator", policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.RequireRole("Administrator");
                });
                options.AddPolicy(
                   "UserMustBeAdministrator",
                   policyBuilder =>
                   {
                       policyBuilder.RequireAuthenticatedUser();
                       policyBuilder.AddRequirements(
                         new UserMustBeAdministratorRequirement("Administrator"));
                   });
            });

            services.AddScoped<IAuthorizationHandler, UserMustBeAdministratorRequirementHandler>();
            services.AddScoped<IUserInfoService, UserInfoService>();

            services.AddTransient<IProfileRepository, ProfileRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("default");
            app.UseMaintainCorsHeaders();

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    public static class MaintainCorsHeadersExtensions
    {
        /// <summary>
        /// Ensure all CORS headers remain or else add them back in ...
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMaintainCorsHeaders(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<MaintainCorsHeadersMiddleware>();
        }

    }
    public class MaintainCorsHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        static MaintainCorsHeadersMiddleware()
        {

        }

        public MaintainCorsHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // Find and hold onto any CORS related headers ...
            var corsHeaders = new HeaderDictionary();
            foreach (var pair in httpContext.Response.Headers)
            {
                if (!pair.Key.StartsWith("access-control-", StringComparison.OrdinalIgnoreCase))
                {
                    continue; // Not CORS related
                }
                corsHeaders[pair.Key] = pair.Value;
            }

            // Bind to the OnStarting event so that we can make sure these CORS headers are still included going to the client
            httpContext.Response.OnStarting(o =>
            {
                var ctx = (HttpContext)o;
                var headers = ctx.Response.Headers;
                // Ensure all CORS headers remain or else add them back in ...
                foreach (var pair in corsHeaders)
                {
                    if (headers.ContainsKey(pair.Key))
                    {
                        continue; // Still there!
                    }
                    headers.Add(pair.Key, pair.Value);
                }
                return Task.CompletedTask;
            }, httpContext);

            // Call the pipeline ...
            await _next(httpContext);
        }
    }
}
