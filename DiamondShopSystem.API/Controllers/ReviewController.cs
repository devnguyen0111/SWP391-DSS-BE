using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services.Users;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("addReview")]
        public IActionResult AddReview([FromBody] Review review)
        {
            var result = _reviewService.AddReview(review);
            if (result == null)
            {
                return BadRequest("Failed to add review.");
            }
            return Ok(result);
        }

        [HttpPut("updateReview")]
        public IActionResult UpdateReview(int reviewId)
        {
            try
            {

                var result = _reviewService.UpdateReview(reviewId);
                return Ok(result);
            } catch (Exception ex) {
                return BadRequest("Failed to update review.");}
        }

        [HttpDelete("deleteReview")]
        public IActionResult DeleteReview(int reviewId)
        {
             try { 
                _reviewService.DeleteReview(reviewId);
                return Ok("Review deleted successfully.");
            } catch (Exception ex) {
                return BadRequest("Failed to delete review.");
            }
            
        }
    }
}
