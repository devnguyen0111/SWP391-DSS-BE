using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repository.Users;

namespace Repository.Products
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DIAMOND_DBContext _dbcontext;
        public CustomerRepository(DIAMOND_DBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public Customer GetCustomer(int id)
        {
            return _dbcontext.Customers.Include(c => c.Cus).FirstOrDefault(c => c.CusId == id)!;
        }
        public void deleteCustomer(int id)
        {
            User u = _dbcontext.Users.FirstOrDefault(c => c.UserId == id)!;
            if (u != null)
            {
                u.Status = "disabled";
                _dbcontext.SaveChanges();
            }
        }
        public Customer addCustomer(Customer customer)
        {
            _dbcontext.Add(customer);
            _dbcontext.SaveChanges(true);
            return customer;
        }
        public Address addAddress(Address address)
        {
            _dbcontext.Addresses.Add(address);
            _dbcontext.SaveChanges();
            return address;

        }
        public Address getCustomerAddress(int id)
        {
            return _dbcontext.Addresses.FirstOrDefault(c => c.AddressId == id);
        }
        public void updateAddress(int id,string street,string city,string state,string zipcode)
        {
            Address address = getCustomerAddress(id);
            address.Street = street;
            address.City = city;
            address.State = state;
            address.ZipCode = zipcode;
            _dbcontext.Addresses.Update(address);
            _dbcontext.SaveChanges();
        }

        public string getEmail(int id)
        {
            return _dbcontext.Users.FirstOrDefault(c => c.UserId == id).Email;
        }

        public Customer updateCustomer(Customer customer)
        {
            var tracker = _dbcontext.Attach<Customer>(customer);
            tracker.State = EntityState.Modified;
            _dbcontext.SaveChanges();
            return customer;
        }
    }
}
