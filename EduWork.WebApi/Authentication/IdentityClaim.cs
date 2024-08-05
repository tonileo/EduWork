using System.Security.Claims;

namespace EduWork.WebApi.Authentication
{
    internal class IdentityClaim : IIdentityClaim
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityClaim(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

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
