using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services.Users;
using Services.Products;
using DiamondShopSystem.API.DTO;
using System.Reflection.Metadata.Ecma335;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;

        public ReviewController(IReviewService reviewService, ICustomerService customerService, IProductService productService)
        {
            _reviewService = reviewService;
            _customerService = customerService;
            _productService = productService;
        }

        [HttpGet("getReviewByProduct")]
        public IActionResult GetReviewByProduct(int productId)
        {
            var result = _reviewService.GetReviewByProduct(productId);
            var count = _reviewService.GetReviewByProduct(productId).Count();
            decimal avarageRating;
            if (result == null || result.Count == 0)
            {
                avarageRating = 0; // No reviews, return 0 as average rating
            }
            else
            {
                avarageRating = (decimal)result.Average(r => r.Rating);
            }
            var _result = result.Select(c =>
            {
                return new ReviewRespone
                {
                    name = c.Cus.CusFirstName+" "+c.Cus.CusLastName,
                    date = (DateOnly)c.ReviewDate,
                    review = c.Review1,
                    rate = (decimal)c.Rating,
                };
            }).ToList();
            if (result == null)
            {
                return NotFound("No reviews found for this product.");
            }
            return Ok(new { _result, Rating = GetRatingPercentages(productId),count ,avarageRating});
        }
        private Dictionary<decimal, double> GetRatingPercentages(int productId)
        {
            var reviews = _reviewService.GetReviewByProduct(productId);
            if (reviews == null || reviews.Count == 0)
            {
                return null;
            }

            var totalReviews = reviews.Count;
            var ratingCounts = reviews.GroupBy(r => r.Rating)
                                      .Select(g => new { Rating = g.Key, Count = g.Count() })
                                      .ToDictionary(g => g.Rating.Value, g => g.Count);

            var ratingPercentages = new Dictionary<decimal, double>();
            for (decimal i = 1; i <= 5; i++)
            {
                ratingPercentages[i] = ratingCounts.ContainsKey(i) ? (ratingCounts[i] / (double)totalReviews) * 100 : 0;
            }

            return ratingPercentages;
        }
        [HttpPost("addReview")]
        public IActionResult AddReview([FromBody] AddReview review)
        {
            try
            {
                var customer = _customerService.GetCustomer(review.CusId);
                if (customer == null)
                {
                    return BadRequest("Customer not found.");
                }

                var product = _productService.GetProductById(review.ProductId);
                if (product == null)
                {
                    return BadRequest("Product not found.");
                }

                var newReview = new Review
                {
                    Review1 = review.ReviewContent,
                    Rating = review.Rating,
                    ReviewDate = review.ReviewDate,
                    CusId = review.CusId,
                    ProductId = review.ProductId
                };

                _reviewService.AddReview(newReview);
                return Ok("Review added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add review. Error: {ex.Message}");
            }
        }

        [HttpPut("updateReview")]
        public IActionResult UpdateReview([FromBody] UpdateReview review)
        {
            try
            {
                var customer = _customerService.GetCustomer(review.CusId);
                if (customer == null)
                {
                    return BadRequest("Customer not found.");
                }

                var product = _productService.GetProductById(review.ProductId);
                if (product == null)
                {
                    return BadRequest("Product not found.");
                }

                var reviewToUpdate = new Review
                {
                    ReviewId = review.ReviewId,
                    Review1 = review.ReviewContent,
                    Rating = review.Rating,
                    ReviewDate = review.ReviewDate,
                    CusId = review.CusId,
                    ProductId = review.ProductId
                };

                _reviewService.UpdateReview(reviewToUpdate);
                return Ok("Review updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update review. Error: {ex.Message}");
            }
        }

        [HttpDelete("deleteReview")]
        public IActionResult DeleteReview(int reviewId)
        {
            try
            {
                var result = _reviewService.DeleteReview(reviewId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to delete review.");
            }

        }
    }
}