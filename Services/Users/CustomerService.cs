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
    }
}
