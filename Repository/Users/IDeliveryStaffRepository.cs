using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.Users.DeliveryStaffRepository;

namespace Repository.Users
{
    public interface IDeliveryStaffRepository
    {
        IEnumerable<DeliveryStaff> GetDeliveryStaffByManagerId(int saleStaffId);
        IEnumerable<DeliveryStaffStatus> GetAllDeliveryStaff();
        IEnumerable<DeliveryStaffStatus> GetDeliveryStaffStatus(int managerId);
    }
}
