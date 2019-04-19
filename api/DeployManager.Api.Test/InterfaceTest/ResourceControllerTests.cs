using DeployManager.Api;
using DeployManager.Api.Entities;
using DeployManager.Api.Helper;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DeployManager.Test
{
    public class ResourceControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ResourceControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetResourceTypes()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/resource/type");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var resourceTypes = JsonConvert.DeserializeObject<ResourceTypeResponse>(stringResponse);
            Assert.Equal(2, resourceTypes.DeployTypes.Count);
            Assert.Equal(default(ServerType).Select((_) => 1).Count(), resourceTypes.ServerTypes.Count);
        }
    }
}
