using System.IO;
using System.Security.Principal;
using DeployManager.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using DeployManager.Api.Services;

namespace DeployManager.Api
{
    public static class HostingEnvironmentExtenstions
    {
        public static bool IsTestEnvironment(this IHostingEnvironment env)
            => env.IsEnvironment("Test");
    }

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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<DeployManagerContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DeployManagerConnection")));

            services.AddScoped<ResourceService>();
            services.AddScoped<BatchReservationService>();
            services.AddScoped<ReservationService>();
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

            app.UseHttpsRedirection();
            app.UseMvc();

            if (!env.IsTestEnvironment())
            {
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
}
