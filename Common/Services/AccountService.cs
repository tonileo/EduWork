using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Common.Contracts;
using Common.DTO;

namespace Common.Services
{
    public class AccountService : IAccount
    {
        private readonly HttpClient httpClient;

        public AccountService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

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
