using Model.Models;
using Repository.Users;

namespace Services.Users
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public Customer GetCustomer(int id)
        {
            return _repository.GetCustomer(id);
        }

        public Address getCustomerAddress(int id)
        {

            return _repository.getCustomerAddress(id);
        }
        public void updateAddress(int id, string street, string city, string state, string zipcode)
        {
            _repository.updateAddress(id, street, city, state, zipcode);
        }
        public string getmail(int id)
        {
            return _repository.getEmail(id);
        }

        public Customer addCustomer(Customer customer)
        {
            return _repository.addCustomer(customer);
        }

        public Customer updateCustomer(Customer customer)
        {
            return _repository.updateCustomer(customer);
        }

        public Address addAddress(Address address)
        {
            return _repository.addAddress(address);
        }
    }
}
