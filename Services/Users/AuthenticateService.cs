using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.Models;
using Repository.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services.Users
{
    public class AuthenticateService : IAuthenticateService
    {
        //dependency injection
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IDictionary<string, string> _credentials = new Dictionary<string, string>();
        public AuthenticateService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public string Authenticate(string email, string password)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
                return null;

            var token = GenerateJwtToken(user,30);
            _credentials[token] = user.Email;
            return token;
        }
        public string googleAuthen(string email)
        {
            var user = _userRepository.GetByEmail(email);
            var token = GenerateJwtToken(user, 30);
            _credentials[token] = user.Email;
            return token;
        }
        public User GetUserByMail(string email)
        {
            return _userRepository.GetByEmail(email);
        }public User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }
        public void Logout(string token)
        {
            _credentials.Remove(token);
        }
        //use to generate token for user authentication
        private string GenerateJwtToken(User user,int expireTime)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("Role", user.Role),  // Add the role claim
            new Claim("UserID",user.UserId.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expireTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public void UpdateUserPassword(User user)
        {
            _userRepository.Update(user);
        }
    }
}
