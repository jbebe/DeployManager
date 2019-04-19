using System;
using System.Collections.Generic;

namespace DeployManager.Api.Models
{
    public partial class User
    {
        public User()
        {
            Reservation = new HashSet<Reservation>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }

        public ICollection<Reservation> Reservation { get; set; }
    }
}
