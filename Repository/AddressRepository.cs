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
    public class AddressRepository : IAddressRepository
    {
        private readonly DIAMOND_DBContext _dbContext;
        public AddressRepository(DIAMOND_DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Address AddAddress(Address address)
        {
            _dbContext.Add(address);
            _dbContext.SaveChanges();
            return address;
        }
        public void UpdateAddress(Address address)
        {
            _dbContext.Entry(address).State = EntityState.Modified;
        }
        public Address getAdress(int id)
        {
            return _dbContext.Addresses.FirstOrDefault(c => c.AddressId == id)!;
        }
    }
}
