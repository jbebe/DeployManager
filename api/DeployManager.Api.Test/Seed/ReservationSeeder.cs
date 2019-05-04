using DeployManager.Api.Helper;
using DeployManager.Commons;
using DeployManager.Test.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeployManager.Test.Seed
{
    internal class ReservationSeeder: IDatabaseSeeder
    {
        public async Task SeedAsync(Api.Models.DeployManagerContext db)
        {
            await Task.CompletedTask;
            //// Seed with different deploy/server type and different time
            //await db.Reservation.AddRangeAsync(
            //    new Api.Models.Reservation()
            //    {
            //        Id = Generator.RandomString(32),
            //        DeployType = DeployType.DevelopmentStaging.NumericValue(),
            //        ServerType = ServerType.AccountApi.NumericValue(),
            //        BranchName = "",
            //        UserId = "",
            //        Start = DateTime.UtcNow,
            //        End = DateTime.UtcNow.AddDays(2),
            //    }
            //);
            //
            //await db.SaveChangesAsync();
        }
    }
}
