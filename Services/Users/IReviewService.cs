using Model.Models;

namespace Services.Users
{
    public interface IReviewService
    {
        public Review AddReview(Review review);

        public Review UpdateReview(int reviewId);

        public void DeleteReview(int reviewId);
    }
}
