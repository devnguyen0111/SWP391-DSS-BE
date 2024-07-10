using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users
{
    public interface IAssignOrderService
    {
        IEnumerable<SaleStaff> GetSaleStaffByManagerId(int managerId);
        IEnumerable<SaleStaff> GetAllSaleStaff();
        IEnumerable<DeliveryStaff> GetDeliveryStaffByManagerId(int managerId);
        IEnumerable<DeliveryStaff> GetAllDeliveryStaff();
        void AssignOrderToStaff(int staffId, int orderId);
    }

}
