using Model.Models;

namespace Repository.Users
{
    public interface IWishlistRepository
    {
        public List<Wishlist> GetAllWishlist(int userId);
        public Wishlist AddWishlist(Wishlist wishlist);

        public Wishlist RemoveWishlist(int wishlistId);
    }
}
