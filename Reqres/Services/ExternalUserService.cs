using Reqres.Clients;
using Reqres.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reqres.Services
{
    public class ExternalUserService
    {
        private readonly ReqResApiClient _apiClient;

        public ExternalUserService(ReqResApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _apiClient.GetUserByIdAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = new List<User>();
            int page = 1;
            ApiResponse<User> response;

            do
            {
                response = await _apiClient.GetUsersAsync(page);
                if (response?.Data != null)
                    users.AddRange(response.Data);

                page++;
            } while (response != null && page <= response.TotalPages);

            return users;
        }
    }
}
