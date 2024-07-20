using DAO;
using Model.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Users;
namespace Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly DIAMOND_DBContext _context;
        public CartRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }
        public Cart getCartFromCustomer(int id)
        {
            return _context.Carts.Include(c => c.CartProducts).ThenInclude(c => c.Product).FirstOrDefault(c => c.CartId == id);
        }
        public List<Cart> getCarts() { return _context.Carts.ToList(); }
        public CartProduct UpdateCartAsync(int cartId, int productId, int quantity)
        {
            var cart =  _context.Carts
                .Include(c => c.CartProducts)
                    .ThenInclude(cp => cp.Product)
                .FirstOrDefault(c => c.CartId == cartId);

            if (cart == null)
                throw new Exception("Cart not found");

            var cartProduct = cart.CartProducts.FirstOrDefault(cp => cp.ProductId == productId);

            if (cartProduct == null)
                throw new Exception("Product not found in cart");

            cartProduct.Quantity = quantity;
             _context.SaveChanges();
            return cartProduct;
        }
        public void RemoveFromCart(int cartID, int pid)
        {
            var cart =  _context.Carts
           .Include(c => c.CartProducts)
               .ThenInclude(cp => cp.Product)
           .FirstOrDefault(c => c.CartId == cartID);

            if (cart == null)
                throw new Exception("Cart not found");

            var cartProduct = cart.CartProducts.FirstOrDefault(cp => cp.ProductId == pid);

            if (cartProduct == null)
                throw new Exception("Product not found in cart");

            cart.CartQuantity -= 1;
            cart.CartProducts.Remove(cartProduct);

             _context.SaveChangesAsync();
        }
        public void ClearCartAsync(int cartId)
        {
            var cart =  _context.Carts
                .Include(c => c.CartProducts)
                .FirstOrDefault(c => c.CartId == cartId);

            if (cart == null)
                throw new Exception("Cart not found");

            cart.CartProducts = new List<CartProduct>();
            cart.CartQuantity = 0;

             _context.SaveChangesAsync();
        }
        public CartProduct AddToCartAsync(int cartId, int productId, int quantity)
        {
            var cart =  _context.Carts
                .Include(c => c.CartProducts)
                    .ThenInclude(cp => cp.Product)
                .FirstOrDefault(c => c.CartId == cartId);
            if (cart == null)
                throw new Exception("Cart not found");
            var product =  _context.Products.Find(productId);
            if (product == null)
                throw new Exception("Product not found");
            var cartProduct = cart.CartProducts.FirstOrDefault(cp => cp.ProductId == productId);
            if (cartProduct == null)
            {
                cartProduct = new CartProduct
                {
                    CartId = cartId,
                    ProductId = productId,
                    Quantity = quantity
                };
                cart.CartProducts.Add(cartProduct);
                
                    cart.CartQuantity += 1;
            }
            else
            {
                cartProduct.Quantity += quantity;
            }
             _context.SaveChanges();
            return cartProduct;
        }
        public Cart createCart(int id)
        {
            Cart c = new Cart
            {
                CartId = id,
                CartQuantity = 0
            };
            _context.Carts.Add(c);
            _context.SaveChanges();
            return c;
        }
        public void SaveCart(Cart cart)
        {
            var existingCart = _context.Carts
                .Include(c => c.CartProducts)
                .FirstOrDefault(c => c.CartId == cart.CartId);

            if (existingCart == null)
            {
                _context.Carts.Add(cart);
            }
            else
            {
                existingCart.CartProducts.Clear();
                foreach (var cartProduct in cart.CartProducts)
                {
                    existingCart.CartProducts.Add(cartProduct);
                }
                _context.Carts.Update(existingCart);
            }
            _context.SaveChanges();
        }
        
    }
}
