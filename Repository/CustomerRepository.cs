using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
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
            return _dbcontext.Customers.FirstOrDefault(c => c.CusId == id)!;
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
    }
}
