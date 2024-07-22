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

        public List<Review> GetReviewByProduct(int productId)
        {
            return _reviewRepository.GetReviewByProduct(productId);
        }
        public bool HasReview(int productId, int customerId)
        {
            return _reviewRepository.HasReview(productId, customerId);
        }
        public Review AddReview(Review review)
        {
            return _reviewRepository.AddReview(review);
        }

        public Review UpdateReview(Review review)
        {
            return _reviewRepository.UpdateReview(review);
        }

        public Review DeleteReview(int reviewId)
        {
            return _reviewRepository.DeleteReview(reviewId);
        }
        public List<Review> getAll()
        {
            return _reviewRepository.getAll();
        }
    }
}