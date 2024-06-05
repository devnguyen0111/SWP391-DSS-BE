using Model.Models;

namespace Repository.Charge
{
    public interface IPaypalRepository
    {
        Task<Order> GetOrderByIdAsync(int orderId);
        Task UpdateOrderStatusAsync(int orderId, string status);
    }
}
