using System;
using System.Net.Http;
using System.Threading.Tasks;
using Reqres;
using Reqres.Clients;
using Reqres.Services;

class Program
{
    static async Task Main(string[] args)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress= new Uri("https://reqres.in/api/");
        httpClient.DefaultRequestHeaders.Add("x-api-key", "reqres-free-v1");

        var apiClient = new ReqResApiClient(httpClient);
        var userService = new ExternalUserService(apiClient);

        var users = await userService.GetAllUsersAsync();

        foreach (var user in users)
        {
            Console.WriteLine($"{user.id}: {user.first_name} {user.last_name}");
        }
    }
}
