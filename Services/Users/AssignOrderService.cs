using Model.Models;
using Repository.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users
{
    public class AssignOrderService : IAssignOrderService
    {
        private readonly ISaleStaffRepository _saleStaffRepository;
        private readonly IDeliveryStaffRepository _deliveryStaffRepository;

        public AssignOrderService(ISaleStaffRepository saleStaffRepository, IDeliveryStaffRepository deliveryStaffRepository)
        {
            _saleStaffRepository = saleStaffRepository;
            _deliveryStaffRepository = deliveryStaffRepository;
        }

        public IEnumerable<SaleStaff> GetSaleStaffByManagerId(int managerId)
        {
            return _saleStaffRepository.GetSaleStaffByManagerId(managerId);
        }
        public IEnumerable<DeliveryStaff> GetDeliveryStaffByManagerId(int managerId)
        {
            return _deliveryStaffRepository.GetDeliveryStaffByManagerId(managerId);
        }
        public IEnumerable<DeliveryStaff> GetAllDeliveryStaff()
        {
            return _deliveryStaffRepository.GetAllDeliveryStaff();
        }
        public IEnumerable<SaleStaff> GetAllSaleStaff()
        {
            return _saleStaffRepository.GetAllSaleStaff();
        }

        public void AssignOrderToStaff(int staffId, int orderId)
        {
            _saleStaffRepository.AssignOrderToStaff(staffId, orderId);
            _saleStaffRepository.Save();
        }
        public class StaffRequest
        {
            public int? StaffId { get; set; }
            public int? Count { get; set; }
            public string? StaffStatus { get; set; }
            public string? Name { get; set; }
            public string? Phone { get; set; }
            public string? Email { get; set; }
            public int? ManagerId { get; set; }
        }
    }

}
