using DeployManager.Api.Helper;
using DeployManager.Test.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace DeployManager.Test.Seed
{
    internal class ServerTypeSeed: IDatabaseSeeder
    {
        public async Task SeedAsync(Api.Models.DeployManagerContext db)
        {
            db.ServerType.AddRange(
                default(ServerType).Select((type) => new Api.Models.ServerType()
                {
                    Id = type.NumericValue(),
                    Name = type.StringValue(),
                    Description = $"{type.StringValue()} is a service. Its id is {type.NumericValue()}",
                }).ToArray()
            );

            await db.SaveChangesAsync();
        }
    }
}
