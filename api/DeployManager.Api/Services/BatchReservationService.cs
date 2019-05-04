using DeployManager.Api.ApiEntities;
using DeployManager.Api.Helper;
using DeployManager.Api.Models;
using DeployManager.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployManager.Api.Services
{
    public class BatchReservationService
    {
        private DeployManagerContext Db { get; }

        public BatchReservationService(DeployManagerContext context)
        {
            Db = context;
        }

        public async Task<CreateBatchReservationResponse> CreateBatchReservationAsync(CreateBatchReservationRequest request)
        {
            var batchId = Generator.RandomString(32);
            var response = new CreateBatchReservationResponse()
            {
                Id = batchId,
                Reservations = new List<string>(),
            };

            foreach (var serverType in request.ServerTypes)
            {
                var start = request.Start.ParseApiString();
                var end = request.End.ParseApiString();
                var reservation = new Reservation()
                {
                    Id = Generator.RandomString(32),
                    DeployType = request.DeployType,
                    ServerType = serverType,
                    BranchName = request.BranchName,
                    UserId = request.UserId,
                    Start = start.Value,
                    End = end.Value,
                };
                Db.Reservation.Add(reservation);
                response.Reservations.Add(reservation.Id);

                var batch = new BatchReservation()
                {
                    BatchId = batchId,
                    ReservationId = reservation.Id,
                };
                Db.BatchReservation.Add(batch);
            };

            await Db.SaveChangesAsync();

            return response;
        }
    }
}
