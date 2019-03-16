using DeployManager.Service.Entities;
using static Common.Utils.DateHelper;

namespace DeployManager.Api.Entities
{
    public class ReservationResponse
    {
        public DeployType DeployType { get; set; }

        public ServerType ServerType { get; set; }

        public string BranchName { get; set; }

        public string Author { get; set; }

        public DateTimeInterval ReservedInterval { get; set; }

        public static ReservationResponse Create(ReservationInfoEntity entity)
        {
            return new ReservationResponse
            {
                BranchName = entity.BranchName,
                Author = entity.Author,
                ReservedInterval = entity.ReservedInterval,
                ServerType = entity.ServerType,
                DeployType = entity.DeployType,
            };
        }
    }
}
