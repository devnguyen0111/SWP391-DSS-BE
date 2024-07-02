using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Orders
{
    public interface IShippingRepository
    {
        Task<Shipping> GetByIdAsync(int shippingId);
       /* Task<List<Shipping>> GetShippingByStatusAsync(string status); */                                 //Get Shipping by status
        Task CreateAsync(Shipping shipping);                                                           //Assign Order cho Staff, sau đó sẽ tự động add vào shipping
        Task<List<Order>> GetOrdersBySaleStaffIdAndStatusAsync(int saleStaffId, string status);        //Lấy list orders trong bảng Shipping ra cho staff, cùng với status "Pending"
        Task<Order> GetOrderByOrderIdAsync(int orderId);                                               // Lấy Order Detail trong bảng Shipping
        Task AssignOrderToDeliveryAsync(int orderId, int deliveryStaffId);                             // Staff assign cho delivery đồng thời đổi status thành "Shipping"
        //Task UpdateAsync(Shipping shipping);
        //Task DeleteAsync(int shippingId);
        // Add more methods as needed
    }


}
