using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Users
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly DIAMOND_DBContext _context;

        public ManagerRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }

        public IEnumerable<Manager> GetManagers()
        {
            return _context.Managers.Include(m => m.DeliveryStaffs).Include(m => m.SaleStaffs).ToList();
        }

        public Manager GetManagerById(int managerId)
        {
            return _context.Managers
                .Include(m => m.DeliveryStaffs)
                .Include(m => m.SaleStaffs)
                .FirstOrDefault(m => m.ManId == managerId);
        }
    }

}
