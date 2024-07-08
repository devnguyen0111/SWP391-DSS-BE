using Model.Models;
using Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrdersManagement
{
    public interface IShippingService
    {
        Task<List<Shipping>> GetAllShippingAsync();
        Task<List<Shipping>> GetShippingByStatusAsync(string status);
        Task<Shipping> GetShippingByIdAsync(int shippingId);
        Task<Shipping> AssignOrderAsync(string status, int orderId, int saleStaffId);
        Task<List<Order>> GetOrdersBySaleStaffIdAndStatusAsync(int saleStaffId, string status);
        Task<Order> GetOrderByOrderIdAsync(int orderId);
        Task AssignShippingToDeliveryAsync(int orderId, int deliveryStaffId);
        Task<bool> IsConfirmFinishShippingAsync(int shippingId);
        //Task UpdateShippingAsync(Shipping shipping);
        //Task DeleteShippingAsync(int shippingId);
    }

}
