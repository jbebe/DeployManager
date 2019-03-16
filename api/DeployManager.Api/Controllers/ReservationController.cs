using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeployManager.Api.Entities;
using DeployManager.Service.Entities;
using DeployManager.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeployManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        public ServerInfoService ServerInfo { get; }
        public ReservationService Reservation { get; }

        public ReservationController(ServerInfoService serverInfo, ReservationService reservation)
        {
            ServerInfo = serverInfo;
            Reservation = reservation;
        }

        // GET api/reservation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationResponse>>> Get([FromQuery] ReservationQueryRequest request = null)
        {
            var response = await Reservation.GetReservationsAsync(request.ServerType, request.DeployType, request.From, request?.To);

            return response.Select(ReservationResponse.Create).ToList();
        }

        // POST api/reservation
        [HttpPost]
        public ActionResult Post([FromBody] ReservationRequest request)
        {
            Reservation.SetReservationAsync(request);

            return new EmptyResult();
        }
    }
}