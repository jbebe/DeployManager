using DeployManager.Api.Helper;
using DeployManager.Api.Models;

namespace DeployManager.Api.ApiEntities
{
    public class GetReservationResponse: CreateReservationRequest
    {
        public string Id { get; set; }

        public static GetReservationResponse Create(Reservation reservation)
            => new GetReservationResponse()
            {
                Id = reservation.Id,
                DeployType = reservation.DeployType,
                ServerType = reservation.ServerType,
                BranchName = reservation.BranchName,
                UserId = reservation.UserId,
                Start = reservation.Start.ToApiDate(),
                End = reservation.End.ToApiDate(),
            };

        public Reservation ToReservation()
        {
            var start = Start.ParseApiDate();
            var end = End.ParseApiDate();
            return new Reservation()
            {
                Id = Id,
                DeployType = DeployType,
                ServerType = ServerType,
                BranchName = BranchName,
                UserId = UserId,
                Start = start.Value,
                End = end.Value,
            };
        }
    }
}
