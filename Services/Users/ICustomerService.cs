using Model.Models;

namespace Services.Users
{
    public interface ICustomerService
    {
        Customer GetCustomer(int id);
    }
}