﻿using DeployManager.Test.Entities;
using System.Threading.Tasks;

namespace DeployManager.Test.Seed
{
    internal class DeployTypeSeeder: IDatabaseSeeder
    {
        public async Task SeedAsync(Api.Models.DeployManagerContext db)
        {
            await db.DeployType.AddRangeAsync(
                new Api.Models.DeployType()
                {
                    Id = (int)DeployType.Production,
                    Name = "Production",
                    Description = "Live slot. Everything here is reachable for the users.",
                    Available = false,
                },
                new Api.Models.DeployType()
                {
                    Id = (int)DeployType.ProductionStaging,
                    Name = "ProdStaging",
                    Description = "Staging slot for live. Everything here is either for final testing or waiting for swap.",
                    Available = false,
                },
                new Api.Models.DeployType()
                {
                    Id = (int)DeployType.Development,
                    Name = "Development",
                    Description = "Development slot. Everything here is being tested before releasing to production.",
                    Available = false,
                },
                new Api.Models.DeployType()
                {
                    Id = (int)DeployType.DevelopmentStaging,
                    Name = "DevelStaging",
                    Description = "Staging slot for development. Only half-done new features appear here.",
                    Available = true,
                }
            );

            await db.SaveChangesAsync();
        }
    }
}
