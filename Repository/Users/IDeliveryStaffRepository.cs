using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Users
{
    public interface IDeliveryStaffRepository
    {
        IEnumerable<DeliveryStaff> GetDeliveryStaffByManagerId(int saleStaffId);
        IEnumerable<DeliveryStaff> GetAllDeliveryStaff();
    }
}
