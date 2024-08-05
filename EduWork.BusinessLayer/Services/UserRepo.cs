using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Contracts;
using EduWork.DataAccessLayer;
using EduWork.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EduWork.BusinessLayer.Services
{
    public class UserRepo : IUser
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;

        public UserRepo( AppDbContext appDbContext, IConfiguration configuration)
        {
            this.appDbContext = appDbContext;
            this.configuration = configuration;
        }

        public async Task<LoginResponse> LoginUserAsync(LoginDto loginDto)
        {
            var getUser = await FindUserbyEmail(loginDto.Email!);

            if (getUser == null) return new LoginResponse(false, "User not found");

            bool checkPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, getUser.Password);
            if (checkPassword) 
                return new LoginResponse(true, "Login succes", GenerateJWTToken(getUser));
            else 
                return new LoginResponse(false, "Invalid credentials");
        }

        private string GenerateJWTToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username!),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> FindUserbyEmail(string email) =>
            await appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<RegistrationResponse> RegistrationUserAsync(RegisterUserDto registrationUserDto)
        {
            var getUser = await FindUserbyEmail(registrationUserDto.Email!);
            if(getUser != null)
                return new RegistrationResponse(false, "user exist");

            appDbContext.Users.Add(new User()
            {
                Username = registrationUserDto.Username,
                Email = registrationUserDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registrationUserDto.Password),
                AppRoleId = 1
            });

            await appDbContext.SaveChangesAsync();
            return new RegistrationResponse(true, "Registration succes");
        }
    }
}
