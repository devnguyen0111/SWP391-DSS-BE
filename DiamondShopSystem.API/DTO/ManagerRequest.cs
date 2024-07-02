namespace DiamondShopSystem.API.DTO
{
    public class ManagerRequest
    {
        public int ManId { get; set; }

        public string ManName { get; set; }

        public string ManPhone { get; set; }

        public int SaleStaffId  {  get; set; }

        public int DeliveryStaffId { get; set; }
    }
}
