using DeployManager.Api.ApiEntities;
using DeployManager.Api.Models;
using DeployManager.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployManager.Api.Services
{
    public class ReservationService
    {
        private DeployManagerContext Db { get; }

        public ReservationService(DeployManagerContext context)
        {
            Db = context;
        }

        public List<GetReservationResponse> QueryReservations(DateTime start, int? deploy, int? server)
            => Db.Reservation.Where((r) =>
                    (r.DeployTypeNavigation.Available) &&
                    (r.Start >= start || r.End >= start) &&
                    (!deploy.HasValue || deploy.Value == r.DeployType) &&
                    (!server.HasValue || server.Value == r.ServerType))
                .Select(GetReservationResponse.Create)
                .ToList();

        public async Task<string> CreateReservationAsync(CreateReservationRequest request)
        {
            var newId = Generator.RandomString(32);

            await Db.Reservation.AddAsync(request.ToReservation(newId));
            await Db.SaveChangesAsync();

            var res = await Db.Reservation.FindAsync(newId);

            return newId;
        }

        public GetReservationResponse GetReservation(string id)
            => Db.Reservation
                .Where((r) => r.Id == id)
                .Select(GetReservationResponse.Create)
                .Single();

        public Task UpdateReservationAsync(UpdateReservationRequest request)
        {
            var entry = Db.Reservation.Update(request.ToReservation());
            return Db.SaveChangesAsync();
        }

        /// <exception cref="InvalidOperationException">When the reservation is not found.</exception>
        public async Task DeleteReservationAsync(string id)
        {
            var reservation = Db.Reservation.Single((r) => r.Id == id);
            Db.Reservation.Remove(reservation);
            await Db.SaveChangesAsync();
        }
    }
}
