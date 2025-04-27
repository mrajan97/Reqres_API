using Reqres.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reqres.Clients
{
    public class ReqResApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions;

        public ReqResApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<ApiResponse<User>> GetUsersAsync(int page)
        {
            var response = await _httpClient.GetAsync($"users");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<User>>(content, _serializerOptions);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"users/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}. {response.ReasonPhrase}");
                    return null;
                }

                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(content);
                var userElement = jsonDoc.RootElement.GetProperty("data");

                return JsonSerializer.Deserialize<User>(userElement.GetRawText(), _serializerOptions);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return null;
            }
        }
    }
}
