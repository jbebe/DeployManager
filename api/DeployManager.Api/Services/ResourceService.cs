using DeployManager.Api.ApiEntities;
using DeployManager.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace DeployManager.Api.Services
{
    public class ResourceService
    {
        private DeployManagerContext Db { get; }

        public ResourceService(DeployManagerContext context)
        {
            Db = context;
        }

        public ResourceTypeResponse GetResourceTypes()
            => new ResourceTypeResponse()
            {
                DeployTypes = Db.DeployType.Where((d) => d.Available).Select(ResourceTypeItemResponse.Create).ToList(),
                ServerTypes = Db.ServerType.Select(ResourceTypeItemResponse.Create).ToList(),
            };

        public List<ServerInstanceResponse> GetServerInstances()
            => Db.ServerInstance
                .Where((i) => i.DeployTypeNavigation.Available)
                .Select(ServerInstanceResponse.Create).ToList();
    }
}
