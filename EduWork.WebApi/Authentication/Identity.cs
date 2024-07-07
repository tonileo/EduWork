using System.Security.Claims;

namespace EduWork.WebApi.Authentication
{
    internal class Identity : IIdentity
    {
        private const string NAME_CLAIM = "name";
        private const string EMAIL_CLAIM = "preferred_username";
        private const string OLD_CLAIM = "";

        private readonly ClaimsPrincipal _principal;

        public Identity(IHttpContextAccessor httpContextAccessor)
        {
            _principal = httpContextAccessor.HttpContext!.User;
        }

        public string? DisplayName
        {

            get
            {
                return _principal.Claims.FirstOrDefault(_ => _.Type == NAME_CLAIM)?.Value;
            }
        }

        public string? Email
        {
            get
            {
                return _principal.Claims.FirstOrDefault(_ => _.Type == EMAIL_CLAIM)?.Value;
            }
        }

        public Guid? ObjectId
        {
            get
            {
                var result = Guid.TryParse(_principal.Claims.FirstOrDefault(_ => _.Type == OLD_CLAIM)?.Value, out var oid);
                return result ? oid : null;
            }
        }
    }
}
