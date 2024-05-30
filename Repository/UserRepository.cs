using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        //Dependency Injection.You can find this in program.cs
        private readonly DIAMOND_DBContext _context;

        public UserRepository(DIAMOND_DBContext context)
        {
            this._context = context;
        }
        public User Add(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            User user = GetById(id);
            if (user != null)
            {
                user.Status = "disabled";
                _context.SaveChanges();
            }
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(c => c.UserId == id);
        }

        public User Update(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

    }
}
