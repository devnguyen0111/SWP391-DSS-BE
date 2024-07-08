using Model.Models;

namespace Services.Users
{
    public interface IOrderService
    {
        Order createOrderDirectly(int uid, int pid, int sid, string address, string phonenum);
        Order createOrderFromCart(int uid, int sid, string address, string phonenum);
        List<Order> getOrderByStatus(int uid, string status);
        decimal GetTotalPrice(Product product);
        List<Order> getOrders(int uid);
        List<Order> getAllOrders();
        void addOrder(Order order);
        List<ShippingMethod> GetShippingMethods();
    }
}