using Model.Models;
using Repository.Users;

namespace Services.Users
{
    public class CartService : ICartService
    {
        private readonly ICartRepository repository;
        public CartService(ICartRepository repository)
        {
            this.repository = repository;
        }
        public Cart GetCartFromCus(int id)
        {
            return repository.getCartFromCustomer(id);
        }
        public List<Cart> GetAll()
        {
            return repository.getCarts();
        }
        public CartProduct AddToCart(int id, int pid)
        {
            return repository.AddToCartAsync(id, pid, 1);
        }
        public CartProduct AddToCartMany(int id, int pid,int quantity)
        {
            return repository.AddToCartAsync(id, pid, quantity);
        }
        public void RemoveFromCart(int id, int pid)
        {
              repository.RemoveFromCart(id, pid);
        }
        public void emptyCart(int id)
        {
            repository.ClearCartAsync(id);
        }
        public CartProduct updateCart(int id, int pid, int quantity)
        {
            return repository.UpdateCartAsync(id, pid, quantity);
        }
        public void saveCart(Cart cart)
        {
            repository.SaveCart(cart);
        }
        public Cart createCart(int id)
        {
            return repository.createCart(id);
        }
    }
}
