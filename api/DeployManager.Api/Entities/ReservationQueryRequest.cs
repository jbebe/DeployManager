using DeployManager.Service.Entities;
using System;

namespace DeployManager.Api.Entities
{
    public class ReservationQueryRequest
    {
        public DeployType DeployType { get; set; }

        public ServerType ServerType { get; set; }

        public DateTime From { get; set; }

        public DateTime? To { get; set; }
    }
}
