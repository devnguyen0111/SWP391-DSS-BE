using Model.Models;

namespace Repository.Users
{
    public interface IReviewRepository
    {
        public List<Review> GetReviewByProduct(int productId);
        public Review AddReview(Review review);

        public Review UpdateReview(Review review);

        public Review DeleteReview(int reviewId);
    }
}