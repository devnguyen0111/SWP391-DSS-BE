using DAO;
using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services;
using Services.Products;
using Services.Users;
using Stripe;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("{id}")]
        public IActionResult getCart(int id)
        {
            var ca = _cartService.GetCartFromCus(id).CartProducts;
            var cartItems = ca.Select(c =>
            {
                return new CartItemRespone
                {
                    pid = c.Product.ProductId,
                    name1 = c.Product.ProductName,
                    price = (decimal)c.Product.UnitPrice,
                    quantity = c.Quantity,
                    size = c.Product.SizeId+"",
                    metal = c.Product.MetaltypeId+"",
                };
            }).ToList();
            CartRespone car = new CartRespone();
            car.items = (List<CartItemRespone>)cartItems;
            return Ok(car);
        }
        [HttpPost("addToCart/{id}")]
        public IActionResult addtoCart(int id,int pid)
        {
            CartProduct p = _cartService.AddToCart(id, pid);
            return Ok(new {p.ProductId,p.Quantity});
        }
        [HttpPost("removeFromCart/{id}")]
        public IActionResult removeFromCart(int id,int pid)
        {
            CartProduct p =_cartService.RemoveFromCart(id, pid);
            return Ok(p);
        }
        [HttpPut("updateCart/{id}")]

        public IActionResult updateCart(int id,int pid,int quantity)
        {
            CartProduct p =_cartService.updateCart(id, pid,quantity);
            return Ok(p);
        }
        [HttpPost("emptyCart/{id}")]

        public IActionResult emptyCart(int id)
        {
            _cartService.emptyCart(id);
            return Ok();
        }
    }
}
