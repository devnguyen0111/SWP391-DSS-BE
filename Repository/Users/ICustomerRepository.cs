using Model.Models;

namespace Repository.Users
{
    public interface ICustomerRepository
    {
        Customer addCustomer(Customer customer);
        void deleteCustomer(int id);
        Customer GetCustomer(int id);
    }
}