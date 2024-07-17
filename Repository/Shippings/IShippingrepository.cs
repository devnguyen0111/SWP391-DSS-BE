using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository.Shippings.ShippingRepository;

namespace Repository.Shippings
{
    public interface IShippingRepository
    {
        Task<List<Shipping>> GetAllShippingAsync();                                                    // Get All Shipping
        Task<List<Shipping>> GetShippingByStatusAsync(string status);                                  // Get Shipping by Status
        Task<Shipping> GetShippingByIdAsync(int shippingId);
       /* Task<List<Shipping>> GetShippingByStatusAsync(string status); */                             // Get Shipping by status
        Task CreateAsync(Shipping shipping);                                                           // Assign Order cho Staff, sau đó sẽ tự động add vào shipping
        Task<List<OrderAssigned>> GetOrdersBySaleStaffIdAndStatusAsync(int saleStaffId, string status);        // Lấy list orders trong bảng Shipping ra cho staff và status
        Task<List<OrderAssigned>> GetOrdersBySaleStaffIdAsync(int saleStaffId);                              // Lấy list orders trong bảng Shipping ra cho staff
        Task<Order> GetOrderByOrderIdAsync(int orderId);                                               // Lấy Order Detail trong bảng Shipping
        Task<Order> AssignShippingToDeliveryAsync(int orderId, int deliveryStaffId);                        // Staff assign cho delivery đồng thời đổi status thành "Shipping"
        Task UpdateShippingAsync(Shipping shipping);                                                    // Update Shipping
        Task<Shipping> GetShippingByOrderIdAsync(int orderId);
        Task<List<OrderAssigned>> GetAllOrdersAsync();
        Task<List<OrderAssigned>> GetOrdersByDeliveryStaffIdAsync(int deliveryStaffId, string status);
        //Task UpdateAsync(Shipping shipping);
        //Task DeleteAsync(int shippingId);
        // Add more methods as needed
    }


}
