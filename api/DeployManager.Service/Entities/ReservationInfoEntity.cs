using static Common.Utils.DateHelper;

namespace DeployManager.Service.Entities
{
    public class ReservationInfoEntity
    {
        public DeployType DeployType { get; set; }

        public ServerType ServerType { get; set; }

        public string BranchName { get; set; }

        public string Author { get; set; }

        public DateTimeInterval ReservedInterval { get; set; }

        public ReservationInfoEntity PreviousReservation { get; set; }
    }
}
