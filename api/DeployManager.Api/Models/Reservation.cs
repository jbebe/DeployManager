using System;
using System.Collections.Generic;

namespace DeployManager.Api.Models
{
    public partial class Reservation
    {
        public Reservation()
        {
            BatchReservation = new HashSet<BatchReservation>();
        }

        public string Id { get; set; }
        public int DeployType { get; set; }
        public int ServerType { get; set; }
        public string BranchName { get; set; }
        public string UserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public DeployType DeployTypeNavigation { get; set; }
        public ServerType ServerTypeNavigation { get; set; }
        public User User { get; set; }
        public ICollection<BatchReservation> BatchReservation { get; set; }
    }
}
