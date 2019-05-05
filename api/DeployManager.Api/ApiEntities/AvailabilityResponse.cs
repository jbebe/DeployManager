using DeployManager.Api.Helper;
using DeployManager.Commons;

namespace DeployManager.Api.ApiEntities
{
    public class AvailabilityResponse
    {
        public string Start { get; set; }

        public string End { get; set; }

        public static AvailabilityResponse FromDateInterval(DateInterval interval)
            => new AvailabilityResponse
            {
                Start = interval.Start.ToApiDate(),
                End = interval.End.ToApiDate(),
            };
    }
}
