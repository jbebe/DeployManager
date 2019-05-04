using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeployManager.Api.ApiEntities;
using DeployManager.Api.Services;
using Microsoft.AspNetCore.Http;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/BatchReservation/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/batch/reservation
        [HttpPost]
        public async Task<ActionResult<CreateBatchReservationResponse>> CreateBatchReservation([FromBody] CreateBatchReservationRequest request)
        {
            var response = await Service.CreateBatchReservationAsync(request);

            return response;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
