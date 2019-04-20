using System.Threading.Tasks;

namespace DeployManager.Test.Seed
{
    interface IDatabaseSeeder
    {
        Task SeedAsync(Api.Models.DeployManagerContext db);
    }
}
