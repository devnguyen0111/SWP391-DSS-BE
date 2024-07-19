using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Repository.Users
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DIAMOND_DBContext _context;
        public OrderRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }

        public List<Order> getOrders()
        {
            return _context.Orders.Include(c => c.ProductOrders).ThenInclude(c => c.Product).ToList();
        }
        public List<Order> getOrderby(int uid, string status)
        {
            return _context.Orders.Include(c => c.ProductOrders).ThenInclude(c => c.Product)
            .Where(o => o.Status == status && o.CusId == uid).ToList();
        }
        public List<Order> getOrders(int uid)
        {
            return _context.Orders.Include(c => c.ProductOrders).ThenInclude(c => c.Product)
            .Where(o =>  o.CusId == uid).ToList();
        }
        public void createOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        public void updateOrder(int oid, string status)
        {
            _context.Orders.FirstOrDefault(c => c.OrderId == oid).Status = status;
            _context.SaveChanges();
        }
        public List<ShippingMethod> GetShippingMethods()
        {
            return _context.ShippingMethods.ToList();
        }
        public Order GetOrderByIdAndStatus(int orderId, string status)
        {
            return _context.Orders
                .Include(o => o.ProductOrders)
                .Include(o => o.Shippings)
                .Include(o => o.ShippingMethod)
                .FirstOrDefault(o => o.OrderId == orderId && o.Status == status);
        }

        public Order GetOrderByOrderId(int orderId)
        {
            return _context.Orders
                .FirstOrDefault(o => o.OrderId == orderId);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<string> CancelOrderAsync(int orderId, int userId)
        {
            //check the order
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return "err1";
            }
            //check which user is canceling the order
            var user =  _context.Users.FirstOrDefault(u => u.UserId == userId);
            
            //if customer, than base on the shipping method to count the time
            if (user.Role.Equals("customer"))
            {
                // Get the current date and time
                DateTime currentDate = DateTime.Now;

                // Calculate the difference between the current date and the order date
                TimeSpan timeDifference = currentDate - order.OrderDate;

                switch (order.ShippingMethodId)
                {
                    //if Standard Shipping, no more than 12 hours
                    case 1:
                        if(timeDifference.TotalHours >= 12)
                        {
                            return "err2";
                        }
                        else { break; }
                    //if Express Shipping, no more than 24 hours
                    case 2:
                        if (timeDifference.TotalHours >= 24)
                        {
                            return "err2";
                        }
                        else { break; }
                    //if Free Shipping, no more than 6 hours
                    case 3:
                        if (timeDifference.TotalHours >= 6)
                        {
                            return "err2";
                        }
                        else { break; }
                }
            }        
            //if Sale Staff / Manager, then just cancel
            order.Status = "Cancel";
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return "";
        }
    }
}
