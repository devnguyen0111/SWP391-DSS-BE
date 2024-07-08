using Model.Models;
using DAO;
using Microsoft.EntityFrameworkCore;

namespace Repository.Users
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DIAMOND_DBContext _dbContext;

        public ReviewRepository(DIAMOND_DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Review> GetReviewByProduct(int productId)
        {
            //check if no review for the product return no review for this product
            var reviews = _dbContext.Reviews.Include(c => c.Cus).Where(r => r.ProductId == productId).ToList();
            if (reviews.Count == 0)
            {
                return null;
            }
            return reviews;
        }
        public bool HasReview(int productId, int customerId)
        {
            return _dbContext.Reviews.Any(r => r.ProductId == productId && r.CusId == customerId);
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

        public Review DeleteReview(int reviewId)
        {
            var review = _dbContext.Reviews.FirstOrDefault(r => r.ReviewId == reviewId);

            if (review == null)
            {
                throw new ArgumentException("Review not found.");
            }

            _dbContext.Reviews.Remove(review);
            _dbContext.SaveChanges();

            return review;
        }

        public Review UpdateReview(Review review) { 
            var reviewToUpdate = _dbContext.Reviews.FirstOrDefault(r => r.ReviewId == review.ReviewId);
            if (reviewToUpdate == null)
            {
                throw new ArgumentException("Review not found.");
            }
            reviewToUpdate.Review1 = review.Review1;
            reviewToUpdate.Rating = review.Rating;
            reviewToUpdate.ReviewDate = review.ReviewDate;
            reviewToUpdate.CusId = review.CusId;
            reviewToUpdate.ProductId = review.ProductId;
            _dbContext.SaveChanges();
            return reviewToUpdate;
        }
        

    }
}