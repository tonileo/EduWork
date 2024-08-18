using Common.DTO.Authentication;

namespace Common.DTO.Contracts
{
    public interface IUser
    {
        Task<RegistrationResponse> RegistrationUserAsync(RegisterUserDto registrationUserDto);
        Task<LoginResponse> LoginUserAsync(LoginDto loginDto);
    }
}
