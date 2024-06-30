using ChatWeb.Core.Interfaces.Repositories;
using ChatWeb.Core.Interfaces.Services;
using ChatWeb.Core.Models;
using ChatWeb.Infrastructure.Data;
using ChatWeb.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        private static readonly string salt = "tryguessingme";

        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration) 
        {
            this._userRepository = userRepository;
            this._configuration = configuration;
        }

        public Tuple<int, string> Login(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);
         
            if (user == null || !VerifyPasswordHash(password, user.HashedPassword))
            {
                throw new Exception("Email or Password is incorrect");
            }
            
            //Generating JWT token for authorization purpose
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Email) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Issuer"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Tuple.Create(user.Id, tokenString);
        }

        public void Register(string email, string password)
        {
            var existingUser = _userRepository.GetUserByEmail(email);

            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }

            string hashedPassword = CreatePasswordHash(password);

            User user = ConvertToUser(email, hashedPassword);

            //store user information
            _userRepository.AddUser(user);  
        }

        private string CreatePasswordHash(string password)
        {
            string salt = _configuration["PasswordHashSettings:Salt"];

            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(salt)))
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = hmac.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            string salt = _configuration["PasswordHashSettings:Salt"];

            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(salt)))
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = hmac.ComputeHash(passwordBytes);
                var storedHashBytes = Convert.FromBase64String(storedHash);
                return hashBytes.SequenceEqual(storedHashBytes);
            }
        }

        public User ConvertToUser(string email, string hashedPassword)
        {
            User user = new User();

            user.Email = email;
            user.HashedPassword = hashedPassword;

            return user;
        }
    
    }
}
