using Model.Models;

namespace Services
{
    public interface ICustomerService
    {
        Customer GetCustomer(int id);
        Address getCustomerAddress(int id);
        void updateAddress(int id, string street, string city, string state, string zipcode);
        string getmail(int id);
    }
}