﻿using DeployManager.Service;
using DeployManager.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Security.Principal;
using System.Text;

namespace DeployManager.Api
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
            services.AddAuthentication(HttpSysDefaults.AuthenticationScheme);
            services.AddSingleton<ServerInfoService>();
            services.AddSingleton<ReservationService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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

            app.Use(async (context, next) =>
            {
                var user = (WindowsIdentity)context.User.Identity;

                if (!user.IsAuthenticated)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("No access");
                    return;
                }

                await next();
            });

            app.UseHttpsRedirection();
            app.UseMvc();
            
            string staticFilePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "web", "dist");
            app.UseFileServer(new FileServerOptions
            {
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false,
                FileProvider = new PhysicalFileProvider(staticFilePath),
                RequestPath = "",
            });

        }
    }
}
