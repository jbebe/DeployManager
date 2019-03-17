using Common.Utils;
using System;

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
                Name = val.ToString("G")
            };
    }
}
