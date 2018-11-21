using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataIdentityServer;
using DataIdentityServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer
{
    public class Startup
    {
        private OwinConfiguration _owinConfiguration;
        public IHostingEnvironment HostingEnvironment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            HostingEnvironment = env;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            _owinConfiguration = new OwinConfiguration();
            Configuration.GetSection("OwinConfiguration").Bind(_owinConfiguration);

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<IdentityServerDb>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy
                        .WithOrigins(_owinConfiguration.WithOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(cfg =>
            {
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<IdentityServerDb>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<ApplicationUser>();
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
                app.Run(async (context) =>
                {
                    await context.Response.WriteAsync("/Home/Error");
                });
            }

            app.UseCors("default");

            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseMvcWithDefaultRoute();
        }
    }
}
