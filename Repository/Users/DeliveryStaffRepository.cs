using DAO;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Users
{
    public class DeliveryStaffRepository : IDeliveryStaffRepository
    {
        private readonly DIAMOND_DBContext _context;

        public DeliveryStaffRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }
        public IEnumerable<DeliveryStaff> GetDeliveryStaffByManagerId(int managerId)
        {
                return _context.DeliveryStaffs.Where(s => s.ManagerId == managerId).ToList();
            
        }
        public IEnumerable<DeliveryStaff> GetAllDeliveryStaff()
        {
                return _context.DeliveryStaffs.ToList();
            
        }


    }
}
