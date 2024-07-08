using Model.Models;

namespace Services.Users
{
    public interface IWishlistService
    {
        List<Wishlist> GetAllWishlist(int userId);
        Wishlist AddWishlist(Wishlist wishlist);
        Wishlist RemoveWishlist(int wishlistId);
    }
}
