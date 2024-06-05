using Model.Models;

namespace Repository.Charge
{
    public interface IVnPayRepository
    {
        void SaveOrder(Order order);
        Order GetOrderById(int orderId);
    }
}
