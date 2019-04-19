using DeployManager.Api.Entities;
using DeployManager.Api.Helper;
using System.Linq;

namespace DeployManager.Test.Seed
{
    internal class ServerTypeSeed: IDatabaseSeeder
    {
        public void Seed(Api.Models.DeployManagerContext db)
        {
            db.ServerType.AddRange(
                default(ServerType).Select((type) => new Api.Models.ServerType()
                {
                    Id = type.NumericValue(),
                    Name = type.StringValue(),
                    Description = $"{type.StringValue()} is a service. Its id is {type.NumericValue()}",
                }).ToArray()
            );
        }
    }
}
