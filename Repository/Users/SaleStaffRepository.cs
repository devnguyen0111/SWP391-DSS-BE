using DAO;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Users
{
    public class SaleStaffRepository : ISaleStaffRepository
    {
        private readonly DIAMOND_DBContext _context;

        public SaleStaffRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }

        public IEnumerable<SaleStaff> GetSaleStaffByManagerId(int managerId)
        {
            return _context.SaleStaffs.Where(s => s.ManagerId == managerId).ToList();
        }

        public SaleStaff GetSaleStaffById(int id)
        {
            return _context.SaleStaffs.Find(id);
        }

        public void AssignOrderToStaff(int staffId, int orderId)
        {
            var staff = GetSaleStaffById(staffId);
            if (staff != null)
            {
                var shipping = new Shipping { OrderId = orderId, SaleStaffId = staffId, DeliveryStaffId = 1, Status = "Pending" };
                staff.Shippings.Add(shipping);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }

}
