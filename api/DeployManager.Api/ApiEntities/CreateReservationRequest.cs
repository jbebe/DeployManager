using System;
using DeployManager.Api.Helper;
using DeployManager.Api.Models;

namespace DeployManager.Api.ApiEntities
{
    public class CreateReservationRequest: IRequestValidator
    {
        public int DeployType { get; set; }
        public int ServerType { get; set; }
        public string BranchName { get; set; }
        public string UserId { get; set; }
        public string Start { get; set; }
        public string End { get; set; }

        public Reservation ToReservation(string id)
        {
            var start = Start.ParseApiString();
            var end = End.ParseApiString();
            return new Reservation()
            {
                Id = id,
                DeployType = DeployType,
                ServerType = ServerType,
                BranchName = BranchName,
                UserId = UserId,
                Start = start.Value,
                End = end.Value,
            };
        }

        public void Validate()
        {
            var start = Start.ParseApiString();
            if (start == null)
            {
                throw new Exception("Invalid start date format!");
            }

            var end = End.ParseApiString();
            if (end == null)
            {
                throw new Exception("Invalid end date format!");
            }
        }
    }
}
