using Model.Models;
using Repository;
using Repository.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;

        public AdminService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string ChangeStatusUser(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user != null)
            {
                switch (user.Status.ToLower())
                {
                    case "active":
                        user.Status = "disabled";
                        _userRepository.Update(user);
                        return "disabled";
                    case "disabled":
                        user.Status = "active";
                        _userRepository.Update(user);
                        return "active";
                    default:
                        return "null";
                }
            }
            else { return "null"; }
        }
    }
}
