using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployManager.Api.ServiceEntities
{
    public class AvailabilityEntity
    {
        public int DeployType { get; set; }

        public List<int> ServerTypes { get; set; }

        public TimeSpan? Duration { get; set; }

        public DateTime? Min { get; set; }

        public DateTime? Max { get; set; }
    }
}
