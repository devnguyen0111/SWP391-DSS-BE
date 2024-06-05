using Model.Models;

namespace Repository.Charge
{
    public interface IStripeRepository
    {
        /*        Task<Order> GetOrderByIdAsync(int orderId);
                Task UpdateOrderStatusAsync(int orderId, string status);*/
        Task<Order> GetOrderByIdAsync(int orderId);
        Task UpdateOrderStatusAsync(int orderId, string status);
    }
}
