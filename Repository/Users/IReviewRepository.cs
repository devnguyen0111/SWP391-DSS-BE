using Model.Models;

namespace Repository.Users
{
    public interface IReviewRepository
    {
        public Review AddReview(Review review);

        public Review UpdateReview(int reviewId);

        public void DeleteReview(int reviewId);
    }
}
