using DeployManager.Api.ApiEntities;
using DeployManager.Api.Helper;
using DeployManager.Api.Models;
using DeployManager.Commons;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task DeleteBatchReservationAsync(string batchId)
        {
            var batchEntries = Db.BatchReservation.Where((b) => b.BatchId == batchId);
            var reservationIds = batchEntries.Select((b) => b.ReservationId);
            var reservations = Db.Reservation.Where((r) => reservationIds.Contains(r.Id));

            Db.BatchReservation.RemoveRange(batchEntries);
            Db.Reservation.RemoveRange(reservations);

            await Db.SaveChangesAsync();
        }

        public List<GetBatchReservationResponse> QueryBatchReservations(DateTime start, int? deployType)
        {
            #if true
            var queryResult = Db.Reservation
                .Join(Db.BatchReservation, (r) => r.Id, (b) => b.ReservationId,
                    (r, b) => new { Batch = b, Reservation = r })
                .GroupBy((br) => br.Batch.BatchId, (br) => br.Reservation)
                .Where((group) => 
                    group.First().Start >= start &&
                    (!deployType.HasValue || group.First().DeployType == deployType.Value))
                .Select((group) => new GetBatchReservationResponse()
                {
                    Id = group.Key,
                    Reservations = group.Select((r) => r.Id).ToList(),
                })
                .ToList();
            #else
            return queryResult2 =
                (from r in Db.Reservation
                 join b in Db.BatchReservation on r.Id equals b.ReservationId
                 select new { b.BatchId, Reservation = r } into rb
                 group rb by rb.BatchId into g
                 where
                     g.First().Reservation.Start >= start &&
                     (!deployType.HasValue || g.First().Reservation.DeployType == deployType.Value)
                 select new GetBatchReservationResponse()
                 {
                     Id = g.Key,
                     Reservations = g.Select(r => r.Reservation.Id).ToList(),
                 }).ToList();
            #endif

            return queryResult;
        }
    }
}
