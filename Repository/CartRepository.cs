using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
using Microsoft.EntityFrameworkCore;
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
        public CartProduct RemoveFromCart(int cartID, int pid)
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
            return cartProduct;
        }
        public void ClearCartAsync(int cartId)
        {
            var cart =  _context.Carts
                .Include(c => c.CartProducts)
                .FirstOrDefault(c => c.CartId == cartId);

            if (cart == null)
                throw new Exception("Cart not found");

            cart.CartProducts.Clear();
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
                cart.CartQuantity += quantity;
            }
            else
            {
                cartProduct.Quantity += quantity;
            }
             _context.SaveChanges();
            return cartProduct;
        }
    }
}
