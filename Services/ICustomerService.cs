using Model.Models;

namespace Services
{
    public interface ICustomerService
    {
        Customer GetCustomer(int id);
    }
}