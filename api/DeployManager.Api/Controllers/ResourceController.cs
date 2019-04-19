using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DeployManager.Api.Entities;
using DeployManager.Api.Helper;

namespace DeployManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        // GET api/resource/type
        [HttpGet("type")]
        public ActionResult<ResourceTypeResponse> GetResourceType()
        {

            return new ResourceTypeResponse
            {
                DeployTypes = default(DeployType).Select(ResourceTypeItemResponse.Create),
                ServerTypes = default(ServerType).Select(ResourceTypeItemResponse.Create),
            };
        }
    }
}