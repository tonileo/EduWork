using System.Net.Http.Json;
using Common.Contracts;
using Common.DTO.Authentication;

namespace Common.Services
{
    public class AccountService(HttpClient httpClient) : IAccount
    {
        public async Task<RegistrationResponse> RegisterAccountAsync(RegisterUserDto model)
        {
            var response = await httpClient.PostAsJsonAsync("api/User/register", model);
            var result = await response.Content.ReadFromJsonAsync<RegistrationResponse>();
            return result!;
        }

        public async Task<LoginResponse> LogInAccountAsync(LoginDto model)
        {
            var response = await httpClient.PostAsJsonAsync("api/User/login", model);
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result!;
        }
    }
}
