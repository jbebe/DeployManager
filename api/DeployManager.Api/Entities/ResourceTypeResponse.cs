using DeployManager.Api.Helper;
using System;
using System.Collections.Generic;

namespace DeployManager.Api.Entities
{
    public class ResourceTypeItemResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static ResourceTypeItemResponse Create<T>(T val) where T : Enum
            => new ResourceTypeItemResponse
            {
                Id = val.NumericValue(),
                Name = val.StringValue(),
            };
    }

    public class ResourceTypeResponse
    {
        public IEnumerable<ResourceTypeItemResponse> DeployTypes { get; set; }

        public IEnumerable<ResourceTypeItemResponse> ServerTypes { get; set; }
    }
}
