using DeployManager.Api;
using DeployManager.Test.Entities;
using DeployManager.Commons;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using DeployManager.Api.ApiEntities;
using System;
using DeployManager.Api.Helper;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using DeployManager.Test.Helper;
using ExpectedObjects;
using System.Net;

namespace DeployManager.Test
{
    public class EndToEndTests : IClassFixture<DeployManagerWebApiFactory<Startup>>
    {
        private HttpClient Client { get; set; }
        public ServiceProvider Service { get; private set; }

        public EndToEndTests(DeployManagerWebApiFactory<Startup> factory)
        {
            Client = factory.CreateClient();
            Service = factory.Service;
        }

        #region Helpers

        private async Task<string> CreateReservationAsync(
            DateTime start,
            DeployType deploy,
            ServerType server,
            DateTime? end = null
        )
        {
            var request = new CreateReservationRequest()
            {
                DeployType = deploy.NumericValue(),
                ServerType = server.NumericValue(),
                BranchName = Generator.RandomString(10),
                UserId = Generator.RandomString(32),
                Start = start.ToApiDate(),
                End = (end ?? start.AddDays(1)).ToApiDate(),
            };
            var httpResponse = await Client.PostAsync("/api/reservation", request.ToRequestBody());
            httpResponse.EnsureSuccessStatusCode();

            var responseBody = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CreateReservationResponse>(responseBody).Id;
        }

        private async Task<GetReservationResponse> GetReservationAsync(string id)
        {
            var httpResponse = await Client.GetAsync($"/api/reservation/{id}");
            httpResponse.EnsureSuccessStatusCode();
            var stringContent = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GetReservationResponse>(stringContent);
        }

        private async Task CleanUpReservationsAsync()
        {
            using (var scope = Service.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<Api.Models.DeployManagerContext>();
                db.Reservation.RemoveRange(db.Reservation);
                db.BatchReservation.RemoveRange(db.BatchReservation);
                await db.SaveChangesAsync();
            }
        }

        private async Task<CreateBatchReservationResponse> CreateBatchReservationAsync(DateTime start, DateTime end, DeployType deployType, ServerType[] serverTypes)
        {
            var request = new CreateBatchReservationRequest()
            {
                DeployType = deployType.NumericValue(),
                ServerTypes = serverTypes.Select(t => t.NumericValue()).ToList(),
                BranchName = Generator.RandomString(10),
                UserId = Generator.RandomString(10),
                Start = start.ToApiDate(),
                End = end.ToApiDate(),
            };

            var httpResponse = await Client.PostAsync("/api/batch/reservation", request.ToRequestBody());
            httpResponse.EnsureSuccessStatusCode();
            var responseJson = await httpResponse.Content.ReadAsStringAsync();

            return responseJson.DeserializeJson<CreateBatchReservationResponse>();
        }

        private async Task<List<GetBatchReservationResponse>> QueryBatchReservationAsync(DateTime start, DeployType deployType)
        {
            var httpResponse = await Client.GetAsync($"/api/batch/reservation?start={start.ToApiDate()}&deploy={deployType.NumericValue()}");
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            return stringResponse.DeserializeJson<List<GetBatchReservationResponse>>();
        }

        #endregion

        #region Resources

        [Fact]
        public async Task GetResourceTypes_Success()
        {
            // Arrange & Act
            var httpResponse = await Client.GetAsync("/api/resource/type");
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var resourceTypes = JsonConvert.DeserializeObject<ResourceTypeResponse>(stringResponse);
            Assert.Single(resourceTypes.DeployTypes);
            Assert.Equal(default(ServerType).Select((_) => 1).Count(), resourceTypes.ServerTypes.Count);
        }

        [Fact]
        public async Task GetServerInstances_Success()
        {
            // Arrange & Act
            var httpResponse = await Client.GetAsync("/api/resource/instance");
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            // Assert
            httpResponse.EnsureSuccessStatusCode();
            var instances = JsonConvert.DeserializeObject<List<ServerInstanceResponse>>(stringResponse);
            Assert.Equal(default(ServerType).Values().Count(), instances.Count);
            Assert.True(instances.All((r) => r.DeployType == DeployType.DevelopmentStaging.NumericValue()));
        }

        #endregion

        #region Query Reservation

        [Fact]
        public async Task QueryReservations_Success_Start()
        {
            try
            {
                // Arrange
                var now = DateTime.UtcNow;
                // Date from yesterday until now (selected)
                // Date from yesterday until 1 hour before (skipped)
                await CreateReservationAsync(now.AddDays(-1), DeployType.DevelopmentStaging, ServerType.AccountApi, now);
                await CreateReservationAsync(now.AddDays(-1), DeployType.DevelopmentStaging, ServerType.AccountApi, now.AddHours(-1));

                // Act
                var httpResponse = await Client.GetAsync($"/api/reservation?start={now.ToApiDate()}");
                var stringResponse = await httpResponse.Content.ReadAsStringAsync();

                // Assert
                httpResponse.EnsureSuccessStatusCode();
                var reservations = JsonConvert.DeserializeObject<List<GetReservationResponse>>(stringResponse);
                Assert.Single(reservations);
                Assert.Equal(now.ToApiDate().ParseApiDate().Value, reservations.Single().End.ParseApiDate().Value);
            }
            finally
            {
                await CleanUpReservationsAsync();
            }
        }

        [Fact]
        public async Task QueryReservations_Success_Deploy()
        {
            try
            {
                // Arrange
                var now = DateTime.UtcNow;
                await CreateReservationAsync(now, DeployType.DevelopmentStaging, ServerType.AccountApi);
                await CreateReservationAsync(now, DeployType.DevelopmentStaging, ServerType.AccountApi);

                // Act
                var httpResponse = await Client.GetAsync($"/api/reservation?start={now.ToApiDate()}&deploy={DeployType.Development.NumericValue()}");
                var stringResponse = await httpResponse.Content.ReadAsStringAsync();

                // Assert
                httpResponse.EnsureSuccessStatusCode();
                var reservations = JsonConvert.DeserializeObject<List<GetReservationResponse>>(stringResponse);
                Assert.Empty(reservations);
            }
            finally
            {
                await CleanUpReservationsAsync();
            }
        }

        [Fact]
        public async Task QueryReservations_Success_Server()
        {
            try
            {
                // Arrange
                var now = DateTime.UtcNow;
                await CreateReservationAsync(now, DeployType.DevelopmentStaging, ServerType.AccountApi);
                await CreateReservationAsync(now.AddDays(-7), DeployType.DevelopmentStaging, ServerType.DeployServer);
                await CreateReservationAsync(now, DeployType.Development, ServerType.FileServer);

                // Act
                var httpResponse = await Client.GetAsync($"/api/reservation?start={now.ToApiDate()}&server={ServerType.AccountApi.NumericValue()}");
                var stringResponse = await httpResponse.Content.ReadAsStringAsync();

                // Assert
                httpResponse.EnsureSuccessStatusCode();
                var reservations = JsonConvert.DeserializeObject<List<GetReservationResponse>>(stringResponse);
                Assert.Single(reservations);
                Assert.Equal(ServerType.AccountApi.NumericValue(), reservations.Single().ServerType);
            }
            finally
            {
                await CleanUpReservationsAsync();
            }
        }

        #endregion

        #region Reservation Operations

        [Fact]
        public async Task CreateReservation_Success()
        {
            try
            {
                // Arrange
                var now = DateTime.UtcNow;
                var request = new CreateReservationRequest()
                {
                    DeployType = DeployType.DevelopmentStaging.NumericValue(),
                    ServerType = ServerType.AccountApi.NumericValue(),
                    BranchName = Generator.RandomString(10),
                    UserId = Generator.RandomString(32),
                    Start = now.ToApiDate(),
                    End = now.AddDays(5).ToApiDate(),
                };

                // Act
                var httpResponse = await Client.PostAsync("/api/reservation", request.ToRequestBody());

                // Assert
                httpResponse.EnsureSuccessStatusCode();
                var stringResponse = await httpResponse.Content.ReadAsStringAsync();
                var reservationResponse = JsonConvert.DeserializeObject<CreateReservationResponse>(stringResponse);
                var reservationEntityResponse = await GetReservationAsync(reservationResponse.Id);
                request.ToExpectedObject().ShouldMatch(reservationEntityResponse);
            }
            finally
            {
                await CleanUpReservationsAsync();
            }
        }

        [Fact]
        public async Task GetReservation_Success()
        {
            try
            {
                // Arrange
                var now = DateTime.UtcNow;
                var request = new CreateReservationRequest()
                {
                    DeployType = DeployType.DevelopmentStaging.NumericValue(),
                    ServerType = ServerType.AccountApi.NumericValue(),
                    BranchName = Generator.RandomString(10),
                    UserId = Generator.RandomString(32),
                    Start = now.ToApiDate(),
                    End = now.AddDays(1).ToApiDate(),
                };
                var httpResponse = await Client.PostAsync("/api/reservation", request.ToRequestBody());
                var id = (await httpResponse.Content.ReadAsStringAsync()).DeserializeJson<CreateReservationResponse>().Id;

                // Act
                httpResponse = await Client.GetAsync($"/api/reservation/{id}");
                var stringResponse = await httpResponse.Content.ReadAsStringAsync();

                // Assert
                httpResponse.EnsureSuccessStatusCode();
                var reservation = stringResponse.DeserializeJson<GetReservationResponse>();
                request.ToExpectedObject().ShouldEqual(reservation);
            }
            finally
            {
                await CleanUpReservationsAsync();
            }
        }

        [Fact]
        public async Task UpdateReservation_Success()
        {
            try
            {
                // Arrange
                var now = DateTime.UtcNow;
                var request = new CreateReservationRequest()
                {
                    DeployType = DeployType.DevelopmentStaging.NumericValue(),
                    ServerType = ServerType.AccountApi.NumericValue(),
                    BranchName = Generator.RandomString(10),
                    UserId = Generator.RandomString(32),
                    Start = now.ToApiDate(),
                    End = now.AddDays(1).ToApiDate(),
                };
                var httpResponse = await Client.PostAsync("/api/reservation", request.ToRequestBody());
                var id = (await httpResponse.Content.ReadAsStringAsync()).DeserializeJson<CreateReservationResponse>().Id;
                var reservation = await GetReservationAsync(id);
                reservation.Start = now.AddDays(10).ToApiDate();
                reservation.End = now.AddDays(10 + 1).ToApiDate();

                // Act
                httpResponse = await Client.PutAsync($"/api/reservation", reservation.ToRequestBody());

                // Assert
                httpResponse.EnsureSuccessStatusCode();
                var reservationResponse = await GetReservationAsync(id);
                reservation.ToExpectedObject().ShouldEqual(reservationResponse);
            }
            finally
            {
                await CleanUpReservationsAsync();
            }
        }

        [Fact]
        public async Task DeleteReservation_Success()
        {
            try
            {
                // Arrange
                var now = DateTime.UtcNow;
                var id = await CreateReservationAsync(now, DeployType.DevelopmentStaging, ServerType.AccountApi);

                // Act
                var httpResponse = await Client.DeleteAsync($"/api/reservation/{id}");

                // Assert
                httpResponse.EnsureSuccessStatusCode();
                httpResponse = await Client.GetAsync($"/api/reservation/{id}");
                Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            }
            finally
            {
                await CleanUpReservationsAsync();
            }
        }

        #endregion

        #region Batch Reservation Operations

        [Fact]
        public async Task Batch_CreateReservation_Success()
        {
            try
            {
                // Arrange
                var now = DateTime.UtcNow;
                var request = new CreateBatchReservationRequest()
                {
                    DeployType = DeployType.DevelopmentStaging.NumericValue(),
                    ServerTypes = new [] { ServerType.AccountApi, ServerType.MailWorker, ServerType.LoginServer }
                        .Select(t => t.NumericValue()).ToList(),
                    BranchName = Generator.RandomString(10),
                    UserId = Generator.RandomString(10),
                    Start = now.ToApiDate(),
                    End = now.AddDays(2).ToApiDate(),
                };

                // Act
                var httpResponse = await Client.PostAsync("/api/batch/reservation", request.ToRequestBody());

                // Assert
                httpResponse.EnsureSuccessStatusCode();
                var stringResponse = await httpResponse.Content.ReadAsStringAsync();
                var reservationResponse = JsonConvert.DeserializeObject<CreateBatchReservationResponse>(stringResponse);
                Assert.Equal(request.ServerTypes.Count, reservationResponse.Reservations.Count);
                foreach (var reservationId in reservationResponse.Reservations)
                {
                    var reservation = await GetReservationAsync(reservationId);
                    new {
                        request.DeployType,
                        request.BranchName,
                        request.UserId,
                        request.Start,
                        request.End,
                    }.ToExpectedObject().ShouldMatch(reservation);
                }
            }
            finally
            {
                await CleanUpReservationsAsync();
            }
        }

        [Fact]
        public async Task Batch_GetReservation_Success()
        {
            try
            {
                // Arrange
                var start = DateTime.UtcNow;
                var end = start.AddDays(2);
                var serverTypes = new[] { ServerType.AccountApi, ServerType.FileServerWorker };
                var deployType = DeployType.DevelopmentStaging;
                var batchId = (await CreateBatchReservationAsync(start, end, deployType, serverTypes)).Id;
                await CreateBatchReservationAsync(start, end, DeployType.Development, serverTypes);

                // Act
                var httpResponse = await Client.GetAsync($"/api/batch/reservation?start={start.ToApiDate()}&deploy={deployType.NumericValue()}");

                // Assert
                httpResponse.EnsureSuccessStatusCode();
                var stringResponse = await httpResponse.Content.ReadAsStringAsync();
                var batchResponse = stringResponse.DeserializeJson<List<GetBatchReservationResponse>>();
                Assert.Single(batchResponse);
                foreach (var batch in batchResponse)
                {
                    Assert.Equal(batchId, batch.Id);
                    Assert.Equal(serverTypes.Length, batch.Reservations.Count);
                    foreach (var reservationId in batch.Reservations)
                    {
                        var reservation = await GetReservationAsync(reservationId);
                        Assert.Equal(start.ToApiDate(), reservation.Start);
                        Assert.Equal(end.ToApiDate(), reservation.End);
                        Assert.Equal(deployType.NumericValue(), reservation.DeployType);
                    }
                }
            }
            finally
            {
                await CleanUpReservationsAsync();
            }
        }

        [Fact]
        public async Task Batch_DeleteReservation_Success()
        {
            try
            {
                // Arrange
                var start = DateTime.UtcNow;
                var end = start.AddDays(2);
                var serverTypes = new[] { ServerType.AccountApi, ServerType.FileServerWorker };
                var deployType = DeployType.DevelopmentStaging;
                var batchId = (await CreateBatchReservationAsync(start, end, deployType, serverTypes)).Id;
                await CreateBatchReservationAsync(start, end, DeployType.Development, serverTypes);

                // Act
                var httpResponse = await Client.DeleteAsync($"/api/batch/reservation/{batchId}");

                // Assert
                httpResponse.EnsureSuccessStatusCode();
                var batches = await QueryBatchReservationAsync(start, deployType);
                Assert.Empty(batches);
            }
            finally
            {
                await CleanUpReservationsAsync();
            }
        }

        [Fact]
        public async Task Batch_GetAvailability_Success()
        {
            try
            {
                // Arrange
                var start = DateTime.UtcNow;
                var end = start.AddDays(1);
                var serverTypes = new[] { ServerType.AccountApi, ServerType.FileServerWorker };
                var deployType = DeployType.DevelopmentStaging;
                await CreateBatchReservationAsync(start, end, DeployType.DevelopmentStaging, serverTypes);
                await CreateBatchReservationAsync(start.AddDays(1), end.AddDays(1), DeployType.DevelopmentStaging, serverTypes);
                var request = new AvailabilityRequest
                {
                    DeployType = deployType.NumericValue(),
                    ServerTypes = serverTypes.Select((s) => s.NumericValue()).ToList(),
                    Duration = TimeSpan.FromDays(1).ToApiDuration(),
                    Min = start.ToApiDate(),
                    Max = (start + TimeSpan.FromDays(14)).ToApiDate(),
                };

                // Act
                var httpResponse = await Client.PostAsync($"/api/batch/availability", request.ToRequestBody());

                // Assert
                httpResponse.EnsureSuccessStatusCode();
                var availabilities = (await httpResponse.Content.ReadAsStringAsync()).DeserializeJson<List<AvailabilityResponse>>();
                Assert.Single(availabilities);
            }
            finally
            {
                await CleanUpReservationsAsync();
            }
        }

        #endregion
    }
}
