using Microsoft.AspNetCore.Mvc;
using Services.Users;

namespace DiamondShopSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;
        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpGet("getAllWishlist")]
        public IActionResult GetAllWishlist(int userId)
        {
            var result = _wishlistService.GetAllWishlist(userId);
            return Ok(result);
        }

        [HttpPost("addWishlist")]
        public IActionResult AddWishlist(int userId, int productId)
        {
            var wishlist = new Model.Models.Wishlist
            {
                UserId = userId,
                ProductId = productId
            };
            var result = _wishlistService.AddWishlist(wishlist);
            return Ok(result);
        }

        [HttpDelete("removeWishlist")]
        public IActionResult RemoveWishlist(int wishlistId)
        {
            var result = _wishlistService.RemoveWishlist(wishlistId);
            return Ok(result);
        }
    }
}
