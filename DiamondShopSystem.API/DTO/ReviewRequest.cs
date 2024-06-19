namespace DiamondShopSystem.API.DTO
{
    public class ReviewRequest
    {
        public string ReviewContent { get; set; }
        public decimal? Rating { get; set; }
        public DateOnly? ReviewDate { get; set; }
        public int CusId { get; set; }
        public int ProductId { get; set; }
    }

    public class AddReview
    {
        public int CusId { get; set; }

        public int ProductId { get; set; }

        public DateOnly? ReviewDate { get; set; }

        public decimal? Rating { get; set; }

        public string ReviewContent { get; set; }
    }

    public class UpdateReview
    {
        public int ReviewId { get; set; }

        public string ReviewContent { get; set; }

        public decimal? Rating { get; set; }

        public DateOnly? ReviewDate { get; set; }

        public int CusId { get; set; }

        public int ProductId { get; set; }
    }
}
