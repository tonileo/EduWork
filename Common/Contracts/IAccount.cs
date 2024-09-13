using Common.DTO.Authentication;

namespace Common.Contracts
{
    public interface IAccount
    {
        Task<RegistrationResponse> RegisterAccountAsync(RegisterUserDto model);
        Task<LoginResponse> LogInAccountAsync(LoginDto model);
    }
}
