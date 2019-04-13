using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DeployManager.Api.Entities;
using DeployManager.Api.Helper;

namespace DeployManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceTypeController : ControllerBase
    {
        // GET api/resourcetype
        [HttpGet]
        public ActionResult<ResourceTypeResponse> Get()
        {
            return new ResourceTypeResponse
            {
                DeployTypes = default(DeployType).Select(ResourceTypeItemResponse.Create),
                ServerTypes = default(ServerType).Select(ResourceTypeItemResponse.Create),
            };
        }
    }
}