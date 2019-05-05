using DeployManager.Api.Helper;
using DeployManager.Api.ServiceEntities;
using System;
using System.Collections.Generic;

namespace DeployManager.Api.ApiEntities
{
    public class AvailabilityRequest
    {
        public int DeployType { get; set; }

        public List<int> ServerTypes { get; set; }

        public string Duration { get; set; }

        public string Min { get; set; }

        public string Max { get; set; }

        public AvailabilityEntity ToAvailability()
            => new AvailabilityEntity
            {
                DeployType = DeployType,
                ServerTypes = ServerTypes,
                Duration = Duration.ParseApiDuration(),
                Min = Min.ParseApiDate(),
                Max = Max.ParseApiDate(),
            };
    }
}
