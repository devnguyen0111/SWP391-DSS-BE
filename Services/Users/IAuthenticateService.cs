using Model.Models;

namespace Services.Users
{
    public interface IAuthenticateService
    {
        string Authenticate(string email, string password);
        void Logout(string token);
        User GetUserByMail(string email);
        User GetUserById(int id);
        void UpdateUserPassword(User user);
    }
}