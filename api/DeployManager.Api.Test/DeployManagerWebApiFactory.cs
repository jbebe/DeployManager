using System;
using DeployManager.Api;
using DeployManager.Api.Models;
using DeployManager.Test.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DeployManager.Test
{
    public class DeployManagerWebApiFactory<TStartup> : WebApplicationFactory<Startup>
    {
        public ServiceProvider Service { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context (AppDbContext) using an in-memory database for testing.
                services.AddDbContext<DeployManagerContext>(options =>
                {
                    options.UseInMemoryDatabase(options.GetType().Name);
                    options.UseInternalServiceProvider(serviceProvider);
                });

                // Build the service provider.
                Service = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = Service.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<DeployManagerContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<DeployManagerWebApiFactory<TStartup>>>();

                    // Ensure the database is created.
                    db.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with some specific test data.
                        var seeders = new IDatabaseSeeder[] {
                            new DeployTypeSeeder(),
                            new ServerTypeSeeder(),
                            new ServerInstanceSeeder(),
                        };
                        foreach (var seeder in seeders)
                        {
                            seeder.SeedAsync(db).Wait();
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database. Error: {ex.Message}");
                    }
                }
            });
        }
    }
}