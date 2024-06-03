using Model.Models;

namespace Services
{
    public interface ICartService
    {
        CartProduct AddToCart(int id, int pid);
        void emptyCart(int id);
        List<Cart> GetAll();
        Cart GetCartFromCus(int id);
        CartProduct RemoveFromCart(int id, int pid);
        CartProduct updateCart(int id, int pid, int quantity);
    }
}