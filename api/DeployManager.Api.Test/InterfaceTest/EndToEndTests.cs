using DeployManager.Api;
using DeployManager.Api.Entities;
using DeployManager.Api.Helper;
using DeployManager.Test.Entities;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DeployManager.Test
{
    public class EndToEndTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public EndToEndTests(CustomWebApplicationFactory<Startup> factory)
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
            Assert.Single(resourceTypes.DeployTypes);
            Assert.Equal(default(ServerType).Select((_) => 1).Count(), resourceTypes.ServerTypes.Count);
        }
    }
}
