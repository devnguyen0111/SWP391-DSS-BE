using Model.Models;

namespace Repository.Users
{
    public interface ICartRepository
    {
        CartProduct AddToCartAsync(int cartId, int productId, int quantity);
        void ClearCartAsync(int cartId);
        Cart getCartFromCustomer(int id);
        List<Cart> getCarts();
        void SaveCart(Cart cart);
        void RemoveFromCart(int cartID, int pid);
        CartProduct UpdateCartAsync(int cartId, int productId, int quantity);
        Cart createCart(int id);
    }
}