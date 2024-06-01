using Model.Models;

namespace Repository
{
    public interface ICustomerRepository
    {
        Customer addCustomer(Customer customer);
        void deleteCustomer(int id);
        Customer GetCustomer(int id);
    }
}