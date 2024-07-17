using Model.Models;

namespace DiamondShopSystem.API.DTO
{
    public class SaleStaffRequest
    {
        public int SStaffId { get; set; }
        public int Count { get; set; }
        public string? Status { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? ManagerId { get; set; }
    }
}
