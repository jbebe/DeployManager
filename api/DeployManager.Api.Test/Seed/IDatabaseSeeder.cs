using System;
using System.Collections.Generic;
using System.Text;

namespace DeployManager.Test.Seed
{
    interface IDatabaseSeeder
    {
        void Seed(Api.Models.DeployManagerContext db);
    }
}
