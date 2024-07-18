using DAO;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.Users.DeliveryStaffRepository;

namespace Repository.Users
{
    public class SaleStaffRepository : ISaleStaffRepository
    {
        private readonly DIAMOND_DBContext _context;

        public SaleStaffRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }

        public IEnumerable<SaleStaffStatus> GetSaleStaffByManagerId(int managerId)
        {

            var saleStaffStatusList = new List<SaleStaffStatus>();

            var saleStaffList = _context.SaleStaffs.Where(s => s.ManagerId == managerId).ToList();

            foreach (var staff in saleStaffList)
            {
                // Count the number of orders assigned to this sale staff, one sale staff can onnly manage 10 "Pending" orders
                var orderCount = _context.Shippings
                    .Count(s => s.SaleStaffId == staff.SStaffId && s.Order.Status == "Pending");

                // Determine status based on order count
                var status = orderCount >= 10 ? "Busy" : "Available";

                // Create SaleStaffStatus object
                var saleStaffStatus = new SaleStaffStatus
                {
                    Count = orderCount,
                    Status = status,
                    SaleStaff = staff
                };

                saleStaffStatusList.Add(saleStaffStatus);
            }
            return saleStaffStatusList;
        }

        public IEnumerable<SaleStaffStatus> GetAllSaleStaff()
        {
            var saleStaffStatusList = new List<SaleStaffStatus>();

            var saleStaffList = _context.SaleStaffs.ToList();

            foreach (var staff in saleStaffList)
            {
                // Count the number of orders assigned to this sale staff, one sale staff can onnly manage 10 "Pending" orders
                var orderCount = _context.Shippings
                    .Count(s => s.SaleStaffId == staff.SStaffId && s.Order.Status == "Pending");

                // Determine status based on order count
                var status = orderCount >= 10 ? "Busy" : "Available";

                // Create SaleStaffStatus object
                var saleStaffStatus = new SaleStaffStatus
                {
                    Count = orderCount,
                    Status = status,
                    SaleStaff = staff
                };

                saleStaffStatusList.Add(saleStaffStatus);
            }
            return saleStaffStatusList;
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
        public class SaleStaffStatus
        {
            public int Count { get; set; }
            public string? Status { get; set; }
            public SaleStaff SaleStaff { get; set; }
        }
    }

}
