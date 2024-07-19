using Model.Models;

namespace Repository.Users
{
    public interface IOrderRepository
    {
        void createOrder(Order order);
        List<Order> getOrderby(int uid, string status);
        List<Order> getOrders();
        void updateOrder(int oid, string status);
        List<Order> getOrders(int uid);
        List<ShippingMethod> GetShippingMethods();
        Order GetOrderByIdAndStatus(int orderId, string status);
        Order GetOrderByOrderId(int orderId);
        Task UpdateOrderAsync(Order order);
        Task<string> CancelOrderAsync(int orderId, int id);


    }
}