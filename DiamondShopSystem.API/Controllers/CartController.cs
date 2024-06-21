using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Model.Models;

using Services;
using Services.Diamonds;
using Services.Products;
using Services.Users;
using System.Runtime.InteropServices;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICoverService _coverService;
        private readonly IMetaltypeService _metaltypeService;
        private readonly ISizeService _sizeService;
        private readonly IDiamondService _diamondService;    
        private readonly IOrderService _orderService;
        private IProductService _productService;
        public CartController(ICartService cartService, ICoverService coverService, IMetaltypeService metaltypeService, ISizeService sizeService,IDiamondService diamondService,IOrderService orderService,IProductService productService)
        {
            _cartService = cartService;
            _coverService = coverService;
            _metaltypeService = metaltypeService;
            _sizeService = sizeService;
            _diamondService = diamondService;
            _orderService = orderService;
            _productService = productService;
        }
        [HttpGet]
        public decimal getotal(int id)
        {
            var ca = _cartService.GetCartFromCus(id).CartProducts;
            var cartItems = ca.Select(c =>
            {
                return new CartItemRespone
                {
                    pid = c.Product.ProductId,
                    name1 = c.Product.ProductName,
                    price = _orderService.GetTotalPrice(_productService.GetProductById(c.ProductId)),
                    quantity = c.Quantity,
                    size = _sizeService.GetSizeById((int)c.Product.SizeId).SizeValue,
                    metal = _metaltypeService.GetMetaltypeById((int)c.Product.MetaltypeId).MetaltypeName,
                    cover = _coverService.GetCoverById((int)c.Product.CoverId).CoverName,
                    coverPrice = (decimal)_coverService.GetCoverById((int)c.Product.CoverId).UnitPrice + (decimal)_sizeService.GetSizeById((int)c.Product.SizeId).SizePrice + (decimal)_metaltypeService.GetMetaltypeById((int)c.Product.MetaltypeId).MetaltypePrice,
                    diamond = _diamondService.GetDiamondById((int)c.Product.DiamondId).DiamondName,
                    diamondPrice = _diamondService.GetDiamondById((int)c.Product.DiamondId).Price,
                    labor = (decimal)c.Product.UnitPrice,
                };
            }).ToList();
            CartRespone car = new CartRespone();
            car.items = (List<CartItemRespone>)cartItems;
            return car.TotalPrice;
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
                    price = _productService.GetProductTotal(c.ProductId),
                    quantity = c.Quantity,
                    size = _sizeService.GetSizeById((int)c.Product.SizeId).SizeValue,
                    metal = _metaltypeService.GetMetaltypeById((int)c.Product.MetaltypeId).MetaltypeName,
                    cover = _coverService.GetCoverById((int)c.Product.CoverId).CoverName,
                    coverPrice =(decimal) _coverService.GetCoverById((int)c.Product.CoverId).UnitPrice +(decimal) _sizeService.GetSizeById((int)c.Product.SizeId).SizePrice + (decimal)_metaltypeService.GetMetaltypeById((int)c.Product.MetaltypeId).MetaltypePrice,
                    diamond = _diamondService.GetDiamondById((int)c.Product.DiamondId).DiamondName,
                    diamondPrice = _diamondService.GetDiamondById((int)c.Product.DiamondId).Price,
                    labor =(decimal) c.Product.UnitPrice,
                };
            }).ToList();
            CartRespone car = new CartRespone();
            car.items = cartItems;
            return Ok(car);
        }
        [HttpPost("addToCart")]
        public IActionResult addtoCart([FromBody] CartRequest c)
        {
            CartProduct p = _cartService.AddToCart(c.id, c.pid);
            return Ok(new {p.ProductId,p.Quantity});
        }
        [HttpPost("removeFromCart")]
        public IActionResult removeFromCartAsync([FromBody] CartRequest c)
        {
            _cartService.RemoveFromCart(c.id, c.pid);
            return Ok();
        }
        [HttpPut("updateCart")]

        public  IActionResult updateCart(int id,int pid,int quantity)
        {
            CartProduct p =_cartService.updateCart(id, pid,quantity);
            decimal total = getotal(id);
            return Ok(new { p.Quantity, total });
        }
        [HttpPost("emptyCart")]

        public IActionResult emptyCart([FromBody]int id)//sẽ thêm vào order
        {
            _cartService.emptyCart(id);
            return Ok();
        }
    }
}
