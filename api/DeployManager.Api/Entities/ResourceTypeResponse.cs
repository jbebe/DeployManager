using System.Collections.Generic;

namespace DeployManager.Api.Entities
{
    public class ResourceTypeResponse
    {
        public IEnumerable<ResourceTypeItemResponse> DeployTypes { get; set; }

        public IEnumerable<ResourceTypeItemResponse> ServerTypes { get; set; }
    }
}
