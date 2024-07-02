using Model.Models;

namespace Services.Users
{
    public interface ICartService
    {
        CartProduct AddToCart(int id, int pid);
        void emptyCart(int id);
        List<Cart> GetAll();
        Cart GetCartFromCus(int id);
        void RemoveFromCart(int id, int pid);
        void saveCart(Cart cart);
        CartProduct updateCart(int id, int pid, int quantity);
        CartProduct AddToCartMany(int id, int pid, int quantity);
        Cart createCart(int id);
    }
}