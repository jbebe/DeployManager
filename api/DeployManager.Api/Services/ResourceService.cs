using DeployManager.Api.Entities;
using DeployManager.Api.Models;
using System.Linq;

namespace DeployManager.Api.Services
{
    public class ResourceService
    {
        private readonly DeployManagerContext _context;

        public ResourceService(DeployManagerContext context)
        {
            _context = context;
        }

        public ResourceTypeResponse GetResourceTypes()
            => new ResourceTypeResponse()
            {
                DeployTypes = _context.DeployType.Where((d) => d.Available).Select(ResourceTypeItemResponse.Create).ToList(),
                ServerTypes = _context.ServerType.Select(ResourceTypeItemResponse.Create).ToList(),
            };
    }
}
