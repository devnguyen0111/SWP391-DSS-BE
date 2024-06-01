using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;

namespace Repository
{
    public interface IStripeRepository
    {
        /*        Task<Order> GetOrderByIdAsync(int orderId);
                Task UpdateOrderStatusAsync(int orderId, string status);*/
        Task<Order> GetOrderByIdAsync(int orderId);
        Task UpdateOrderStatusAsync(int orderId, string status);
    }
}
