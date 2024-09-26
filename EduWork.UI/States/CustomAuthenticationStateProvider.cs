using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace EduWork.UI.States
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private const string LocalStorageKey = "auth";
        private readonly ILocalStorageService localStorageService;
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());

        private readonly HttpClient httpClient;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            this.localStorageService = localStorageService;
            this.httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await localStorageService.GetItemAsStringAsync(LocalStorageKey)!;
            if (string.IsNullOrEmpty(token) || await IsTokenExpired(token))
            {
                return new AuthenticationState(anonymous);
            }

            var claims = GetClaims(token);
            if (claims == null || !claims.Identity.IsAuthenticated)
            {
                return new AuthenticationState(anonymous);
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return new AuthenticationState(claims);
        }

        private async Task<bool> IsTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken != null && jwtToken.ValidTo < DateTime.UtcNow)
            {
                await localStorageService.RemoveItemAsync(LocalStorageKey);
                return true;
            }

            return false;
        }

        public static ClaimsPrincipal GetClaims(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken)) return null;

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Name)?.Value),
                new Claim(ClaimTypes.Email, token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Email)?.Value)
            };

            var roles = token.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return new ClaimsPrincipal(new ClaimsIdentity(claims, "JwtAuth"));
        }

        public async Task UpdateAuthenticationState(string jwtToken)
        {
            ClaimsPrincipal claims;
            if (!string.IsNullOrEmpty(jwtToken))
            {
                claims = GetClaims(jwtToken);
                if (claims == null || !claims.Identity.IsAuthenticated) return;

                await localStorageService.SetItemAsStringAsync(LocalStorageKey, jwtToken);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            }
            else
            {
                await localStorageService.RemoveItemAsync(LocalStorageKey);
                claims = anonymous;

                httpClient.DefaultRequestHeaders.Authorization = null;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claims)));
        }
    }
}
