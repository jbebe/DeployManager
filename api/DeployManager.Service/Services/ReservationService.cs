using DeployManager.Service.Entities;
using StackExchange.Redis;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DeployManager.Service.Services
{
    public class ReservationService
    {
        const string KeyPrefix = "deploymanager:reservation";

        private ConnectionMultiplexer RedisClient { get; }
        private IDatabase RedisDb { get; }

        public ReservationService()
        {
            RedisClient = ConnectionMultiplexer.Connect("localhost");
            RedisDb = RedisClient.GetDatabase();
        }

        private static string GetKey(Entities.ServerType server, DeployType deploy)
            => $"{KeyPrefix}:{(int)server:D2}_{(int)deploy:D2}";

        public Task<bool> SetReservationAsync(ReservationInfoEntity reservation)
        {
            string key = GetKey(reservation.ServerType, reservation.DeployType);
            double score = reservation.ReservedInterval.From.Ticks;
            string value = JsonConvert.SerializeObject(reservation);

            return RedisDb.SortedSetAddAsync(key, value, score);
        }

        public async Task<IEnumerable<ReservationInfoEntity>> GetReservationsAsync(Entities.ServerType server, DeployType deploy, DateTime from, DateTime? to)
        {
            string key = GetKey(server, deploy);
            double toTicks = to?.Ticks ?? double.PositiveInfinity;
            var result = await RedisDb.SortedSetRangeByScoreAsync(key, from.Ticks, toTicks);

            return result.Select((json) => JsonConvert.DeserializeObject<ReservationInfoEntity>(json));
        }
    }
}
