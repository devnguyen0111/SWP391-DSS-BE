using Model.Models;
using Repository.Users;

namespace Services.Users
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;

        public WishlistService(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public List<Wishlist> GetAllWishlist(int userId)
        {
            return _wishlistRepository.GetAllWishlist(userId);
        }

        public Wishlist AddWishlist(Wishlist wishlist) {
            return _wishlistRepository.AddWishlist(wishlist);
        }

        public Wishlist RemoveWishlist(int wishlistId) { 
            return _wishlistRepository.RemoveWishlist(wishlistId);
        }
    }
}
