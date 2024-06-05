using Model.Models;

namespace Repository.Users
{
    public interface IAddressRepository
    {
        Address AddAddress(Address address);
        Address getAdress(int id);
        void UpdateAddress(Address address);
    }
}