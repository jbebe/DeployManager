using System;
using System.Collections.Generic;

namespace DeployManager.Api.Models
{
    public partial class BatchReservation
    {
        public string BatchId { get; set; }
        public string ReservationId { get; set; }

        public Reservation Reservation { get; set; }
    }
}
