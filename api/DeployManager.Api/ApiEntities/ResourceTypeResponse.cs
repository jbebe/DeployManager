using System.Collections.Generic;

namespace DeployManager.Api.ApiEntities
{
    public class ResourceTypeItemResponse
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public static ResourceTypeItemResponse Create(Models.DeployType deployType)
            => new ResourceTypeItemResponse
            {
                Id = deployType.Id,
                Name = deployType.Name,
                Description = deployType.Description,
            };

        public static ResourceTypeItemResponse Create(Models.ServerType serverType)
            => new ResourceTypeItemResponse
            {
                Id = serverType.Id,
                Name = serverType.Name,
                Description = serverType.Description,
            };
    }

    public class ResourceTypeResponse
    {
        public List<ResourceTypeItemResponse> DeployTypes { get; set; }

        public List<ResourceTypeItemResponse> ServerTypes { get; set; }
    }
}
