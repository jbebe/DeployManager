using System;
using System.Collections.Generic;

namespace DeployManager.Api.Models
{
    public partial class Reservation
    {
        public string Id { get; set; }
        public int DeployType { get; set; }
        public int ServerType { get; set; }
        public string BranchName { get; set; }
        public string Author { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Previous { get; set; }
    }
}
