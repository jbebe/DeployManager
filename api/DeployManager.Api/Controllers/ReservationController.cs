using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeployManager.Api.ApiEntities;
using DeployManager.Api.Helper;
using DeployManager.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeployManager.Api.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        public ReservationService Service { get; }

        public ReservationController(ReservationService service)
        {
            Service = service;
        }

        // GET: api/reservation?start=20190421191800&deploy=1&server=1
        [HttpGet]
        public ActionResult<List<GetReservationResponse>> QueryReservations([FromQuery] string start, [FromQuery] int? deploy, [FromQuery] int? server)
        {
            var startDate = start.ParseApiString();
            if (!startDate.HasValue)
            {
                return new BadRequestResult();
            }

            return Service.QueryReservations(startDate.Value, deploy, server);
        }

        // GET: api/reservation/764352334265
        [HttpGet("{id}")]
        public ActionResult<GetReservationResponse> GetReservation(string id)
        {
            try
            {
                return Service.GetReservation(id);
            }
            catch (InvalidOperationException)
            {
                return new NotFoundResult();
            }
        }

        // POST: api/reservation
        [HttpPost]
        public async Task<ActionResult<CreateReservationResponse>> CreateReservationAsync([FromBody] CreateReservationRequest request)
        {
            var id = await Service.CreateReservationAsync(request);

            return new CreateReservationResponse()
            {
                Id = id
            };
        }

        // PUT: api/reservation
        [HttpPut]
        public async Task UpdateReservationAsync([FromBody] UpdateReservationRequest request)
        {
            await Service.UpdateReservationAsync(request);
        }

        // DELETE: api/reservation/764352334265
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(string id)
        {
            try
            {
                await Service.DeleteReservationAsync(id);
                return new OkResult();
            }
            catch (InvalidOperationException)
            {
                return new NotFoundResult();
            }
        }
    }
}
