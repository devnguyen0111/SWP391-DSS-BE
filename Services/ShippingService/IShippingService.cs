using Model.Models;
using Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.Shippings.ShippingRepository;
using static Repository.Users.DeliveryStaffRepository;
using static Repository.Users.SaleStaffRepository;

namespace Services.ShippingService
{
    public interface IShippingService
    {
        IEnumerable<SaleStaff> GetSaleStaffByManagerId(int managerId);
        IEnumerable<SaleStaffStatus> GetAllSaleStaff();
        IEnumerable<DeliveryStaffStatus> GetDeliveryStaffByManagerId(int managerId);
        IEnumerable<DeliveryStaffStatus> GetAllDeliveryStaff();
        void AssignOrderToStaff(int staffId, int orderId);
        Task<List<Shipping>> GetAllShippingAsync();
        Task<List<Shipping>> GetShippingByStatusAsync(string status);
        Task<Shipping> GetShippingByIdAsync(int shippingId);
        Task<Shipping> AssignOrderAsync(string status, int orderId, int saleStaffId);
        Task<List<OrderAssigned>> GetOrdersBySaleStaffIdAndStatusAsync(int saleStaffId, string status);
        Task<List<OrderAssigned>> GetOrdersBySaleStaffIdAsync(int saleStaffId);
        Task<Order> GetOrderByOrderIdAsync(int orderId);
        Task<Order> AssignShippingToDeliveryAsync(int orderId, int deliveryStaffId);
        Task<bool> IsConfirmFinishShippingAsync(int shippingId);
        Task<List<OrderAssigned>> GetAllOrdersAsync();
        Task<List<OrderAssigned>> GetOrdersByDeliveryStaffIdAsync(int deliveryStaffId, string status);
        //Task UpdateShippingAsync(Shipping shipping);
        //Task DeleteShippingAsync(int shippingId);
    }

}
