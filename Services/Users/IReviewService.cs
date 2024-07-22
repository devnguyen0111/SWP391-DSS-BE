using Model.Models;

namespace Services.Users
{
    public interface IReviewService
    {
        List<Review> GetReviewByProduct(int productId);
        public Review AddReview(Review review);
        bool HasReview(int productId, int customerId);
        public Review UpdateReview(Review review);

        public Review DeleteReview(int reviewId);
        List<Review> getAll();
    }
}