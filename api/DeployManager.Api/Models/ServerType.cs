using System;
using System.Collections.Generic;

namespace DeployManager.Api.Models
{
    public partial class ServerType
    {
        public ServerType()
        {
            Reservation = new HashSet<Reservation>();
            ServerInstance = new HashSet<ServerInstance>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Reservation> Reservation { get; set; }
        public ICollection<ServerInstance> ServerInstance { get; set; }
    }
}
