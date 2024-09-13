using Common.DTO.Authentication;

namespace EduWork.BusinessLayer.Contracts
{
    public interface IUser
    {
        Task<RegistrationResponse> RegistrationUserAsync(RegisterUserDto registrationUserDto);
        Task<LoginResponse> LoginUserAsync(LoginDto loginDto);
    }
}
