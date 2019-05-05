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
                var start = request.Start.ParseApiDate();
                var end = request.End.ParseApiDate();
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

        public List<AvailabilityResponse> GetAvailability(AvailabilityRequest request)
        {
            var week = TimeSpan.FromDays(7);
            var availability = request.ToAvailability();
            availability.Max = (availability.Max ?? DateTime.UtcNow.AddDays(30));
            availability.Duration = availability.Duration ?? TimeSpan.FromDays(1);
            var frameStart = availability.Min ?? DateTime.UtcNow;
            var windowSize = week;
            var frameEnd = frameStart + windowSize;
            var availableInterval = new List<DateInterval>();

            do
            {
                // Get reservations in given timeframe
                var reservations = Db.Reservation
                    .Where((r) =>
                        r.DeployType == availability.DeployType &&
                        availability.ServerTypes.Contains(r.ServerType) &&
                        (
                            (r.Start < frameStart && r.End >= frameStart) || 
                            (r.Start >= frameStart && r.End <= frameEnd) || 
                            (r.Start < frameEnd && r.End > frameEnd))
                        ).ToList();

                // Merge intervals
                var intervals = new List<DateInterval>();
                foreach (var reservation in reservations)
                {
                    var createNewInterval = true;

                    // Check coverage
                    // [   {-------}   ]
                    var removableIntervals = intervals
                        .Where((i) => reservation.Start <= i.Start && reservation.End >= i.End).ToList();
                    foreach (var i in removableIntervals)
                    {
                        intervals.Remove(i);
                    }

                    // Check left side collision
                    // [     {---]-------}
                    var interval = intervals.FirstOrDefault((i) => reservation.End > i.Start && reservation.End < i.End);
                    if (interval != null)
                    {
                        interval.Start = interval.Start.Max(reservation.Start);
                        createNewInterval = false;
                    }

                    // Check right side collision
                    // {-------[---}      ]
                    interval = intervals.FirstOrDefault((i) => reservation.Start > i.Start && reservation.Start < i.End);
                    if (interval != null)
                    {
                        interval.End = interval.End.Max(reservation.End);
                        createNewInterval = false;
                    }

                    // If we did not expand an existing interval, we have to create a new one.
                    if (createNewInterval)
                    {
                        intervals.Add(new DateInterval(reservation.Start, reservation.End));
                    }
                }
                intervals = intervals.OrderBy((i) => i.Start).ToList();
                intervals.Add(new DateInterval(frameEnd.AddSeconds(1), frameEnd));

                // Find free spaces in the normalized interval list
                for (var i = 0; i < intervals.Count; ++i)
                {
                    if (i + 1 >= intervals.Count)
                    {
                        break;
                    }

                    var current = intervals[i];
                    var next = intervals[i+1];

                    var delta = TimeSpan.FromMinutes(1);
                    if (next.Start - current.End >= availability.Duration + (delta * 2))
                    {
                        availableInterval.Add(new DateInterval(current.End + delta, next.Start - delta));
                    }
                }
                frameStart = frameStart + windowSize - availability.Duration.Value;
                frameEnd = frameStart + windowSize;
            }
            while (frameStart < availability.Max);

            return availableInterval.Select(AvailabilityResponse.FromDateInterval).ToList();
        }
    }
}
