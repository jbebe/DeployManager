using Microsoft.AspNetCore.Mvc;
using DeployManager.Api.Services;
using System.Collections.Generic;
using DeployManager.Api.ApiEntities;

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

        // GET api/resource/instance
        [HttpGet("instance")]
        public ActionResult<List<ServerInstanceResponse>> GetServerInstances()
            => Service.GetServerInstances();
    }
}