using Model.Models;

namespace Repository
{
    public interface ICartRepository
    {
        CartProduct AddToCartAsync(int cartId, int productId, int quantity);
        void ClearCartAsync(int cartId);
        Cart getCartFromCustomer(int id);
        List<Cart> getCarts();
        CartProduct RemoveFromCart(int cartID, int pid);
        CartProduct UpdateCartAsync(int cartId, int productId, int quantity);
    }
}