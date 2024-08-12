using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO.Authentication;

namespace Common.DTO.Contracts
{
    public interface IUser
    {
        Task<RegistrationResponse> RegistrationUserAsync(RegisterUserDto registrationUserDto);
        Task<LoginResponse> LoginUserAsync(LoginDto loginDto);
    }
}
