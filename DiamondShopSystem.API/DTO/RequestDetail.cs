namespace DiamondShopSystem.API.DTO
{
    public class RequestDetail
    {
        public int RequestId { get; set; }

        public string Title { get; set; }

        public string ProcessStatus { get; set; }

        public DateTime? RequestedDate { get; set; }

        public string Context { get; set; }

        public string RequestStatus { get; set; }

        public int SStaffId { get; set; }

        public int ManId { get; set; }

        public int OrderId { get; set; }
    }
}
