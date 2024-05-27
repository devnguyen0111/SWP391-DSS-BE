using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class CustomerDAO
    {
        private readonly DIAMOND_DBContext _context;

        public CustomerDAO(DIAMOND_DBContext context)
        {
            _context = context;
        }

        // Method to retrieve all customers
        public List<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        // Method to retrieve a customer by ID
        public Customer GetCustomerById(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.CusId == id);
        }

        // Method to add a new customer
        public Customer AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        // Method to update an existing customer
        public Customer UpdateCustomer(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();
            return customer;
        }
        // Method to delete will add later :P

    }
}
