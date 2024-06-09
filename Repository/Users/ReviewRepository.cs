using Model.Models;
using DAO;

namespace Repository.Users
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DIAMOND_DBContext _dbContext;

        public ReviewRepository(DIAMOND_DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Review AddReview(Review review)
        {
            if (review.Rating > 5)
            {
                throw new ArgumentException("Rating should not exceed 5.");
            }

            review.ReviewDate = DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            _dbContext.Reviews.Add(review);
            _dbContext.SaveChanges();

            return review;
        }

        public Review UpdateReview(int reviewId)
        {
            var review = _dbContext.Reviews.FirstOrDefault(r => r.ReviewId == reviewId);

            if (review == null)
            {
                throw new ArgumentException("Review not found.");
            }

            // Perform the necessary update logic here

            _dbContext.SaveChanges();

            return review;
        }

        public void DeleteReview(int reviewId)
        {
            var review = _dbContext.Reviews.FirstOrDefault(r => r.ReviewId == reviewId);

            if (review == null)
            {
                throw new ArgumentException("Review not found.");
            }

            _dbContext.Reviews.Remove(review);
            _dbContext.SaveChanges();
        }
    }
}
