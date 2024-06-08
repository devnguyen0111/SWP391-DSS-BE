using Model.Models;

namespace Services.Users
{
    public interface ICustomerService
    {
        Customer GetCustomer(int id);
        Address getCustomerAddress(int id);
        void updateAddress(int id, string street, string city, string state, string zipcode);
        Address addAddress(Address address);
        string getmail(int id);
        Customer addCustomer(Customer customer);
        Customer updateCustomer(Customer customer); 

    }
}