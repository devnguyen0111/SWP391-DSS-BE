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
        void AssignOrderToStaff(int staffId, int orderId);
    }

}
