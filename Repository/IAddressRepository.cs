using Model.Models;

namespace Repository
{
    public interface IAddressRepository
    {
        Address AddAddress(Address address);
        Address getAdress(int id);
        void UpdateAddress(Address address);
    }
}