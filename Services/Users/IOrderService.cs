using Model.Models;

namespace Services.Users
{
    public interface IOrderService
    {
        Order createOrderDirectly(int uid, int pid, int sid);
        Order createOrderFromCart(int uid, int sid);
        List<Order> getOrderByStatus(int uid, string status);
        decimal GetTotalPrice(Product product);
    }
}