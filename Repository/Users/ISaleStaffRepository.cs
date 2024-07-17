using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.Users.SaleStaffRepository;

namespace Repository.Users
{
    public interface ISaleStaffRepository
    {
        IEnumerable<SaleStaff> GetSaleStaffByManagerId(int managerId);
        IEnumerable<SaleStaffStatus> GetAllSaleStaff();
        SaleStaff GetSaleStaffById(int id);
        void AssignOrderToStaff(int staffId, int orderId);
        void Save();
    }
}
