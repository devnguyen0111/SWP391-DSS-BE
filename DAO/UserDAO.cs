using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class UserDAO
    {
        //Dependency Injection.You can find this in program.cs
        private readonly DIAMOND_DBContext _context;

        public UserDAO(DIAMOND_DBContext context)
        {
            this._context = context;
        }
        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User UpdateUser(int id, User user)
        {
            User existingUser = GetUser(id);
            if (existingUser != null)
            {
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.Status = user.Status;
                existingUser.Role = user.Role;
                _context.Update(existingUser);
                _context.SaveChanges();
            }
            return existingUser;
        }
        public void DeleteUser(int id)
        {
            User existingUser = GetUser(id);
            if (existingUser != null)
            {
                existingUser.Status = "Disabled"; // Assuming "Disabled" is the status indicating a disabled user
                _context.Update(existingUser);
                _context.SaveChanges();
            }
        }
    }
}
