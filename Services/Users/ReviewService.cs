using Model.Models;
using Repository.Users;

namespace Services.Users
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Review AddReview(Review review)
        {
            return _reviewRepository.AddReview(review);
        }

        public Review UpdateReview(int reviewId)
        {
            return _reviewRepository.UpdateReview(reviewId);
        }

        public void DeleteReview(int reviewId)
        {
            _reviewRepository.DeleteReview(reviewId);
        }
    }
}
