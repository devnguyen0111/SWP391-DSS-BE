using Model.Models;

namespace DiamondShopSystem.API.DTO
{
    public class DeliveryStaffRequest
    {
        public int DStaffId { get; set; }
        public int Count { get; set; }
        public string? Status { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public int? ManagerId { get; set; }
    }
}
