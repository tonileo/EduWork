using System.Security.Claims;

namespace EduWork.WebApi.Authentication
{
    internal class IdentityClaim(IHttpContextAccessor httpContextAccessor) : IIdentityClaim
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string? Email
        {
            get
            {
                var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
                return claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value;
            }
        }
    }
}
