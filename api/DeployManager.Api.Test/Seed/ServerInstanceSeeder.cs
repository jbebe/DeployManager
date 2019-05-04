using DeployManager.Commons;
using DeployManager.Test.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace DeployManager.Test.Seed
{
    internal class ServerInstanceSeeder: IDatabaseSeeder
    {
        public async Task SeedAsync(Api.Models.DeployManagerContext db)
        {
            await db.ServerInstance.AddRangeAsync(
                default(ServerType).Select((type) => new Api.Models.ServerInstance()
                {
                    DeployType = DeployType.DevelopmentStaging.NumericValue(),
                    ServerType = type.NumericValue()
                }).ToArray()
            );

            await db.SaveChangesAsync();
        }
    }
}
