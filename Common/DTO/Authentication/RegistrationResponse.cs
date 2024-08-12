using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Authentication
{
    public record RegistrationResponse(bool Flag, string Message = null!);
}
