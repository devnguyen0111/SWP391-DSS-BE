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

        public IEnumerable<DeliveryStaffStatus> GetDeliveryStaffStatus(int managerId)
        {
            var deliveryStaffStatusList = new List<DeliveryStaffStatus>();

            // Fetch all delivery staff
            var deliveryStaffList = _context.DeliveryStaffs.Where(d => d.ManagerId == managerId).ToList();

            foreach (var staff in deliveryStaffList)
            {
                // Count the number of orders assigned to this delivery staff
                var orderCount = _context.Shippings
                    .Count(s => s.DeliveryStaffId == staff.DStaffId && s.Status == "Shipping");

                // Determine status based on order count
                var status = orderCount >= 10 ? "Busy" : "Available";

                // Create DeliveryStaffStatus object
                var deliveryStaffStatus = new DeliveryStaffStatus
                {
                    Count = orderCount,
                    Status = status,
                    DeliveryStaff = staff
                };

                deliveryStaffStatusList.Add(deliveryStaffStatus);
            }

            return deliveryStaffStatusList;
        }


        public class DeliveryStaffStatus
        {
            public int Count { get; set; }
            public string? Status { get; set; }
            public DeliveryStaff DeliveryStaff { get; set; }
        }

    }
}
