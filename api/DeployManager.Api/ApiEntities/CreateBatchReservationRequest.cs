using System.Collections.Generic;

namespace DeployManager.Api.ApiEntities
{
    public class CreateBatchReservationRequest
    {
        public int DeployType { get; set; }

        public List<int> ServerTypes { get; set; }

        public string BranchName { get; set; }

        public string UserId { get; set; }

        public string Start { get; set; }

        public string End { get; set; }
    }
}
