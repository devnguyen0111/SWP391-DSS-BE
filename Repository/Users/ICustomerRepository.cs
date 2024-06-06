using Model.Models;

namespace Repository.Users
{
    public interface ICustomerRepository
    {
        Customer addCustomer(Customer customer);
        void deleteCustomer(int id);
        Customer GetCustomer(int id);
        Address getCustomerAddress(int id);
         void updateAddress(int id,string street, string city, string state, string zipcode);
        string getEmail(int id);
        Customer updateCustomer(Customer customer);
    }
}