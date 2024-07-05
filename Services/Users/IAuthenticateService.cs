using Model.Models;
using System.Security.Claims;

namespace Services.Users
{
    public interface IAuthenticateService
    {
        string Authenticate(string email, string password);
        string googleAuthen(string email);
        void Logout(string token);
        User GetUserByMail(string email);
        User GetUserById(int id);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        void UpdateUserPassword(User user);
        string GenerateJwtToken(User user, int expireTime);
    }
}