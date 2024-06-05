using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Repository.Users
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
