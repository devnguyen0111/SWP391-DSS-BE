namespace DiamondShopSystem.API.DTO
{
    public class StaffRequest
    {
        public int? StaffId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? ManagerId { get; set; }
    }

    public class AssignOrderRequest
    {
        public int StaffId { get; set; }
        public int OrderId { get; set; }
    }
}