using System.Collections.Generic;

namespace DeployManager.Api.Entities
{
    public class ResourceTypeResponse
    {
        public IEnumerable<ResourceTypeItemResponse> DeployType { get; set; }

        public IEnumerable<ResourceTypeItemResponse> ServerType { get; set; }
    }
}
