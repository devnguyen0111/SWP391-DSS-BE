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

        public AssignOrderService(ISaleStaffRepository saleStaffRepository)
        {
            _saleStaffRepository = saleStaffRepository;
        }

        public IEnumerable<SaleStaff> GetSaleStaffByManagerId(int managerId)
        {
            return _saleStaffRepository.GetSaleStaffByManagerId(managerId);
        }

        public void AssignOrderToStaff(int staffId, int orderId)
        {
            _saleStaffRepository.AssignOrderToStaff(staffId, orderId);
            _saleStaffRepository.Save();
        }
    }

}
