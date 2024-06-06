using Model.Models;

namespace Repository.Users
{
    public interface IUserRepository
    {
        User Add(User entity);
        void Delete(int id);
        User GetById(int id);
        List<User> GetAll();
        User Update(User entity);
        User GetByEmail(string email);
    }
}