using Moq;
using Reqres.Clients;
using Reqres.Models;
using Reqres.Services;
using System.Net.Http;

namespace Reqres_Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1Async()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://reqres.in/api/")
            };
            httpClient.DefaultRequestHeaders.Add("x-api-key", "reqres-free-v1");
            var apiClient = new ReqResApiClient(httpClient);
            var userService = new ExternalUserService(apiClient);

            var expectedUser = new User { id = 1, first_name = "George", last_name = "Bluth", email = "george.bluth@reqres.in" };

            // Act
            var users = await userService.GetAllUsersAsync();

            // Assert
            Assert.NotNull(users.First());
            Assert.AreEqual("George", users.First().first_name.ToString());
        }
    }
}