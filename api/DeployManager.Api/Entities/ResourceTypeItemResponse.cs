using System;

namespace DeployManager.Api.Entities
{
    public class ResourceTypeItemResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static ResourceTypeItemResponse Create<T>(T enumType) where T : Enum
            => new ResourceTypeItemResponse
            {
                Id = int.Parse(enumType.ToString("D")),
                Name = enumType.ToString("G")
            };
    }
}
