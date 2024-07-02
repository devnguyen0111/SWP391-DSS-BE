using Model.Models;
using Repository.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;

        public ManagerService(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public IEnumerable<Manager> GetManagers()
        {
            return _managerRepository.GetManagers();
        }

        public Manager GetManagerById(int managerId)
        {
            return _managerRepository.GetManagerById(managerId);
        }
    }

}
