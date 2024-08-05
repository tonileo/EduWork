using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace EduWork.UI.States
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private const string LocalStorageKey = "auth";
        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());
        private readonly ILocalStorageService localStorageService;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await localStorageService.GetItemAsStringAsync(LocalStorageKey)!;
            if (string.IsNullOrEmpty(token))
                return await Task.FromResult(new AuthenticationState(anonymous));

            var (username, email) = GetClaims(token);
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
                return await Task.FromResult(new AuthenticationState(anonymous));

            var claims = SetClaimPrincipal(username, email);
            if (claims is null)
                return await Task.FromResult(new AuthenticationState(anonymous));
            else
                return await Task.FromResult(new AuthenticationState(claims));
        }

        public static (string, string) GetClaims(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken)) return (null!, null!);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var username = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Name)!.Value;
            var email = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Email)!.Value;

            return (username, email);
        }

        public static ClaimsPrincipal SetClaimPrincipal(string username, string email)
        {
            if (username is null || email is null) return new ClaimsPrincipal();
            return new ClaimsPrincipal(new ClaimsIdentity(
            [
                new(ClaimTypes.Name, username!),
                new(ClaimTypes.Email, email!)
                ], "JwtAuth"));
        }

        public async Task UpdateAuthenticationState(string jwtToken)
        {
            var claims = new ClaimsPrincipal();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                var (username, email) = GetClaims(jwtToken);
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
                    return;

                var setClaims = SetClaimPrincipal(username, email);
                if (setClaims is null)
                    return;

                await localStorageService.SetItemAsStringAsync(LocalStorageKey, jwtToken);
            }
            else
            {
                await localStorageService.RemoveItemAsync(LocalStorageKey);
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claims)));
        }
    }
}
