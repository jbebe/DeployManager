using System.Collections.Generic;
using System.Threading.Tasks;
using DeployManager.Api.ApiEntities;
using DeployManager.Api.Helper;
using DeployManager.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeployManager.Api.Controllers
{
    [Route("api/batch/reservation")]
    [ApiController]
    public class BatchReservationController : ControllerBase
    {
        public BatchReservationService Service { get; }

        public BatchReservationController(BatchReservationService service)
        {
            Service = service;
        }

        // GET: api/BatchReservation
        [HttpGet]
        public ActionResult<List<GetBatchReservationResponse>> QueryBatchReservations([FromQuery] string start, [FromQuery] int? deploy)
        {
            var startDate = start.ParseApiString();
            if (!startDate.HasValue)
            {
                return new BadRequestResult();
            }

            return Service.QueryBatchReservations(startDate.Value, deploy);
        }

        // POST: api/batch/reservation
        [HttpPost]
        public async Task<ActionResult<CreateBatchReservationResponse>> CreateBatchReservation([FromBody] CreateBatchReservationRequest request)
        {
            var response = await Service.CreateBatchReservationAsync(request);

            return response;
        }

        // DELETE: api/batch/reservation/764352334265
        [HttpDelete("{id}")]
        public async Task DeleteBatchReservation([FromRoute] string id)
        {
            await Service.DeleteBatchReservationAsync(id);
        }
    }
}
