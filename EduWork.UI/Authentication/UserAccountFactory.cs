using System.Security.Claims;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

namespace EduWork.UI.Authentication
{
    public class UserAccountFactory : AccountClaimsPrincipalFactory<UserAccount>
    {
        public UserAccountFactory(IAccessTokenProviderAccessor accessor) : base(accessor) 
        {
        }

        public override async ValueTask<ClaimsPrincipal> CreateUserAsync(UserAccount account, RemoteAuthenticationUserOptions options)
        {
            var initialUser = await base.CreateUserAsync(account, options);

            if (initialUser.Identity.IsAuthenticated)
            {
                var userIdentity = (ClaimsIdentity)initialUser.Identity;

                if (account.AuthenticationMethod is not null)
                {
                    foreach (var value in account.AuthenticationMethod)
                    {
                        userIdentity.AddClaim(new Claim("amr", value));
                    }
                }
            }

            return initialUser;
        } 
    }
}
