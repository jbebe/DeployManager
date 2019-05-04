using System.Collections.Generic;

namespace DeployManager.Api.ApiEntities
{
    public class CreateBatchReservationResponse
    {
        public string Id { get; set; }

        public List<string> Reservations { get; set; }
    }
}
