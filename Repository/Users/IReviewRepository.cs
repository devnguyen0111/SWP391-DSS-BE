using Model.Models;

namespace Repository.Users
{
    public interface IReviewRepository
    {
        public List<Review> GetReviewByProduct(int productId);
        public Review AddReview(Review review);

        public Review UpdateReview(Review review);
        bool HasReview(int productId, int customerId);
        public Review DeleteReview(int reviewId);
        List<Review> getAll();
    }
}