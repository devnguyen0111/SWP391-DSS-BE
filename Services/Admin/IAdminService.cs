using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Admin
{
    public interface IAdminService
    {
        string? ChangeStatusUser(int userId);                            //disable or enable specific users (but admin)                       
    }
}
