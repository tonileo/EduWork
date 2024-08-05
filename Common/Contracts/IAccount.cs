using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;

namespace Common.Contracts
{
    public interface IAccount
    {
        Task<RegistrationResponse> RegisterAccountAsync(RegisterUserDto model);
        Task<LoginResponse> LogInAccountAsync(LoginDto model);
    }
}
