using System.Collections.Generic;
using DeployManager.Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DeployManager.Service.Services;

namespace DeployManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServerInfoController : ControllerBase
    {
        public ServerInfoService ServerInfo { get; }

        public ServerInfoController(ServerInfoService serverInfo)
        {
            ServerInfo = serverInfo;
        }

        // GET api/serverinfo
        [HttpGet]
        public ActionResult<IEnumerable<ServerInfoResponse>> Get()
        {
            return ServerInfo
                .GetAllServerInfos()
                .Select(ServerInfoResponse.Create)
                .ToList();
        }
    }
}
