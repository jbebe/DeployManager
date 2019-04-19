using Microsoft.AspNetCore.Mvc;
using DeployManager.Api.Entities;
using DeployManager.Api.Services;

namespace DeployManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        public ResourceService Service { get; }

        public ResourceController(ResourceService service)
        {
            Service = service;
        }

        // GET api/resource/type
        [HttpGet("type")]
        public ActionResult<ResourceTypeResponse> GetResourceTypes()
            => Service.GetResourceTypes();
    }
}