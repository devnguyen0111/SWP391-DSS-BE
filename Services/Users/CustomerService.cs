using Model.Models;
using Repository.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
