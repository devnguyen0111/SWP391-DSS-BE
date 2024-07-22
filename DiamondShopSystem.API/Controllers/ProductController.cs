
using DiamondShopSystem.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Services.Diamonds;
using Services.OtherServices;
using Services.Products;

namespace DiamondShopSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICoverMetaltypeService _coverMetaltypeService;
        private readonly ISizeService _sizeService;
        private readonly IMetaltypeService _metaltypeService;
        private readonly ICoverService _coverService;
        private readonly IDiamondService _diamondService;

        public ProductController(IProductService productService,ICoverMetaltypeService c,ISizeService s,IMetaltypeService mt,ICoverService cv,IDiamondService d)
        {
            _productService = productService;
            _coverMetaltypeService = c;
            _sizeService = s;
            _metaltypeService = mt;
            _coverService = cv;
            _diamondService = d;
        }

        [HttpGet("products")]
        public ActionResult<List<ProductRequest>> GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            var productRequest = products.Select(c =>
            {
                return new ProductRequest
                {
                    ProductId = c.ProductId,
                    ProductName = c.ProductName,
                    PP = c.Pp,
                    status = c.Cover.Status,
                    UnitPrice = c.UnitPrice
                };
            }).ToList();
            return Ok(productRequest);
        }

        [HttpGet("CountProduct")]
        public IActionResult CountProduct()
        {
            int count = _productService.GetAllProducts().Count;
            return Ok(count);
        }

        [HttpGet("productDetail/{id}")]
        public IActionResult GetProductDetailById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            //product.Cover.CoverName + product.Diamond.DiamondName
            var productDetail = new ProductDetail
            {
                ProductId = product.ProductId,
                imgUrl = _coverMetaltypeService.GetCoverMetaltype(product.CoverId, product.MetaltypeId)?.ImgUrl ?? "https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/illustration-gallery-icon_53876-27002.avif?alt=media&token=037e0d50-90ce-4dd4-87fc-f54dd3dfd567",
                categoryName = _productService.GetCategoryById(product.Cover.CategoryId).CategoryName,
                diamondId = product.DiamondId,
                realUnitPrice = (decimal)product.UnitPrice,
                sizeId = product.SizeId,
                coverId = product.CoverId,
                metalTypeId = product.MetaltypeId,
                categoryId = product.Cover.CategoryId,
                CoverStatus = _coverService.DetermineCoverStatus(product.CoverId),
                DiamondStatus = product.Diamond.Status,
                ProductName = product.ProductName,
                DiamondName = product.Diamond.DiamondName,
                CoverName = product.Cover.CoverName,
                MetaltypeName = product.Metaltype.MetaltypeName,
                SizeName = _sizeService.GetSizeById(product.SizeId).SizeValue,
                coverPrice = product.Cover.UnitPrice+_metaltypeService.GetMetaltypeById(product.MetaltypeId).MetaltypePrice+_sizeService.GetSizeById(product.SizeId).SizePrice,
                diamondPrice = product.Diamond.Price,
                carat = product.Diamond.CaratWeight,
                color = product.Diamond.Color,
                cut = product.Diamond.Cut,
                clarity = product.Diamond.Clarity,
                shape = product.Diamond.Shape,
                Pp = product.Pp,
                UnitPrice = _productService.GetProductTotal(product.ProductId),
                productStatus = product.Status,
            };
            
            return Ok(productDetail);
        }
        [HttpGet("getMostSaleProduct")]
        public IActionResult getMostSaleProducrByCate(int count,int cate)
        {
            var products = _productService.getMostSaleProduct(count, cate);
            var productRequest = products.Select(c =>
            {
                return new ProductRequest
                {
                    ProductId = c.ProductId,
                    imgUrl = _coverMetaltypeService.GetCoverMetaltype(_productService.GetProductById(c.ProductId).CoverId, _productService.GetProductById(c.ProductId).MetaltypeId).ImgUrl,
                    ProductName = _productService.GetProductById(c.ProductId).ProductName,
                    UnitPrice =(decimal) _productService.GetProductById(c.ProductId).UnitPrice+
                    _productService.GetProductById(c.ProductId).Diamond.Price+ _productService.GetProductById(c.ProductId).Cover.UnitPrice+
                    _productService.GetProductById(c.ProductId).Size.SizePrice+ _productService.GetProductById(c.ProductId).Metaltype.MetaltypePrice,
                };
            }).ToList();
            return Ok(productRequest);
        }
       
        [HttpGet("getFilteredProductAd")]
        public IActionResult GetFilteredProducts(
    [FromQuery] int? categoryId,
    [FromQuery] int? subCategoryId,
    [FromQuery] int? metaltypeId,
    [FromQuery] int? sizeId,
    [FromQuery] decimal? minPrice,
    [FromQuery] decimal? maxPrice,
    [FromQuery] string? sortOrder,
    [FromQuery] int? pageNumber,
    [FromQuery] int? pageSize,
    [FromQuery] List<int>? sizeIds,
    [FromQuery] List<int>? metaltypeIds,
    [FromQuery] List<string>? diamondShapes)
        {
            var filteredProducts = _productService.FilterProductsAd(
                categoryId,
                subCategoryId,
                metaltypeId,
                sizeId,
                minPrice,
                maxPrice,
                sortOrder,
                sizeIds,
                metaltypeIds,
                diamondShapes,
                pageNumber,
                pageSize).Select(c =>
                {
                    return new ProductRequest
                    {
                        ProductId = c.ProductId,
                        imgUrl = _coverMetaltypeService.GetCoverMetaltype(c.CoverId, c.MetaltypeId)?.ImgUrl ?? "https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/illustration-gallery-icon_53876-27002.avif?alt=media&token=037e0d50-90ce-4dd4-87fc-f54dd3dfd567",
                        ProductName = c.ProductName,
                        UnitPrice = _productService.GetProductTotal(c.ProductId),
                    };
                }).ToList();

            return Ok(filteredProducts);
        }
        [HttpGet("getFilteredProductAdManager")]
        public IActionResult GetFilteredProducts123(
    [FromQuery] int? categoryId,
    [FromQuery] int? subCategoryId,
    [FromQuery] int? metaltypeId,
    [FromQuery] int? sizeId,
    [FromQuery] decimal? minPrice,
    [FromQuery] decimal? maxPrice,
    [FromQuery] string? sortOrder,
    [FromQuery] int? pageNumber,
    [FromQuery] int? pageSize,
    [FromQuery] List<int>? sizeIds,
    [FromQuery] List<int>? metaltypeIds,
    [FromQuery] List<string>? diamondShapes)
        {
            var filteredProducts = _productService.FilterProductsAd(
                categoryId,
                subCategoryId,
                metaltypeId,
                sizeId,
                minPrice,
                maxPrice,
                sortOrder,
                sizeIds,
                metaltypeIds,
                diamondShapes,
                pageNumber,
                pageSize).Select(c =>
                {
                    return new ProductRequest
                    {
                        ProductId = c.ProductId,
                        imgUrl = _coverMetaltypeService.GetCoverMetaltype(c.CoverId, c.MetaltypeId)?.ImgUrl?? "https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/illustration-gallery-icon_53876-27002.avif?alt=media&token=037e0d50-90ce-4dd4-87fc-f54dd3dfd567",
                        ProductName = c.ProductName,
                        UnitPrice = _productService.GetProductTotal(c.ProductId),
                    };
                }).ToList();

            return Ok(filteredProducts);
        }
        //_coverMetaltypeService.GetCoverMetaltype(c.ProductId, c.MetaltypeId).ImgUrl
        //https://firebasestorage.googleapis.com/v0/b/idyllic-bloom-423215-e4.appspot.com/o/illustration-gallery-icon_53876-27002.avif?alt=media&token=037e0d50-90ce-4dd4-87fc-f54dd3dfd567
        [HttpGet("getFilterOption")]
        public IActionResult getFilterOption(int category)
        {
            var Metal = _metaltypeService.getMetalTypeByCate(category).Select(mt =>
            {
                return new metaltypeFilter
                {
                    id = mt,
                    value = _metaltypeService.GetMetaltypeById(mt).MetaltypeName,
                    status = "Available",
                };
            }).ToList();
            var Sizes = _sizeService.getSizeByCate(category).Select(s =>
            {
                return new sizeFilter
                {
                    id = s,
                    value = _sizeService.GetSizeById(s).SizeValue,
                    status = "Available",
                };
            }).ToList();
            var Shape = new List<string> {"Round","Princess","Emerald","Asscher","Cushion","Marquise","Radiant","Oval","Pear","Heart"};
            
            return Ok(new { Metal, Sizes, Shape });
        }
        [HttpGet("getFilterOptionManager")]
        public IActionResult getFilterOption2(int category)
        {
            var Metal = _metaltypeService.getMetalTypeByCate(category).Select(mt =>
            {
                return new metaltypeFilter
                {
                    id = mt,
                    value = _metaltypeService.GetMetaltypeById(mt).MetaltypeName,
                    status = "Available",
                };
            }).ToList();
            var Sizes = _sizeService.getSizeByCate(category).Select(s =>
            {
                return new sizeFilter
                {
                    id = s,
                    value = _sizeService.GetSizeById(s).SizeValue,
                    status = "Available",
                };
            }).ToList();

            return Ok(new { Metal, Sizes });
        }
        //kiểm tra tồn tại chưa
        [HttpPost]
        [Route("addProduct")]
        public IActionResult AddProduct([FromBody] AddProductRequest request)
        {
            // Validate SizeId
            var size = _sizeService.GetSizeById(request.SizeId);
            if (size == null)
            {
                return BadRequest("Invalid SizeId");
            }

            // Validate MetaltypeId
            var metaltype = _metaltypeService.GetMetaltypeById(request.MetaltypeId);
            if (metaltype == null)
            {
                return BadRequest("Invalid MetaltypeId");
            }

            // Fetch Cover and Diamond to concatenate names
            var cover = _coverService.GetCoverById(request.CoverId);
            if (cover == null)
            {
                return BadRequest("Invalid CoverId");
            }

            var diamond = _diamondService.GetDiamondById(request.DiamondId);
            if (diamond == null)
            {
                return BadRequest("Invalid DiamondId");
            }
            
            // Concatenate CoverName and DiamondName to form ProductName
            var productName = cover.CoverName + " " + diamond.DiamondName;

            var product = new Product
            {
                ProductName = productName,
                UnitPrice = request.UnitPrice,
                DiamondId = request.DiamondId,
                CoverId = request.CoverId,
                MetaltypeId = request.MetaltypeId,
                SizeId = request.SizeId,
                Pp = request.Pp
                
            };
            var producthaha = _productService.GetAllProducts().Where(c => c.ProductId != product.ProductId).FirstOrDefault(c => c.MetaltypeId == request.MetaltypeId && c.CoverId == request.CoverId && c.DiamondId == request.DiamondId && c.SizeId == request.SizeId);
            if (producthaha != null)
            {
                return BadRequest("Product with these combinations already exist!");
            }
            product.Status = "Available";
            _productService.AddProduct(product);
            return Ok(product.ProductId);
        }
        //
        [HttpPut]
        [Route("updateProduct")]
        public IActionResult UpdateProduct([FromBody] UpdateProductRequest request)
        {
            var product = _productService.GetProductById(request.ProductId);
            if (product == null)
            {
                return NotFound();
            }
            // Validate SizeId
            var size = _sizeService.GetSizeById(request.SizeId);
            if (size == null)
            {
                return BadRequest("Invalid SizeId");
            }
            // Validate MetaltypeId
            var metaltype = _metaltypeService.GetMetaltypeById(request.MetaltypeId);
            if (metaltype == null)
            {
                return BadRequest("Invalid MetaltypeId");
            }
            // Fetch Cover and Diamond to concatenate names
            var cover = _coverService.GetCoverById(request.CoverId);
            if (cover == null)
            {
                return BadRequest("Invalid CoverId");
            }
            var diamond = _diamondService.GetDiamondById(request.DiamondId);
            if (diamond == null)
            {
                return BadRequest("Invalid DiamondId");
            }
            // Concatenate CoverName and DiamondName to form ProductName
            var productName = cover.CoverName +" "+ diamond.DiamondName;
            product.ProductName = productName;
            product.UnitPrice = request.UnitPrice;
            product.DiamondId = request.DiamondId;
            product.CoverId = request.CoverId;
            product.MetaltypeId = request.MetaltypeId;
            product.SizeId = request.SizeId;
            product.Pp = request.Pp;
            var producthaha = _productService.GetAllProducts().Where(c=>c.ProductId!=product.ProductId).FirstOrDefault(c => c.MetaltypeId == request.MetaltypeId && c.CoverId == request.CoverId && c.DiamondId == request.DiamondId && c.SizeId == request.SizeId);
            if (producthaha != null)
            {
                return BadRequest("Product with these combinations already exist!");
            }
            _productService.UpdateProduct(product);
            return Ok(product);
        }
        [HttpPost("select")]
        public IActionResult SelectProductOptions([FromBody] TempProductSelection selection)
        {
            HttpContext.Session.Set("TempProductSelection", selection);
            return Ok("Product selection saved.");
        }
        [HttpGet("get-selection")]
        public IActionResult GetProductSelection()
        {
            TempProductSelection selection = HttpContext.Session.Get<TempProductSelection>("TempProductSelection");
            return Ok(selection);
        }
        [HttpPost("confirm")]
        public IActionResult ConfirmProduct([FromBody] TempProductSelection selection)
        {

            if (selection == null || !selection.CoverId.HasValue || !selection.MetaltypeId.HasValue || !selection.SizeId.HasValue || !selection.DiamondId.HasValue)
            {
                return BadRequest("Incomplete product selection.");
            }
            var size = _sizeService.GetSizeById(selection.SizeId.Value);
            var metaltype = _metaltypeService.GetMetaltypeById(selection.MetaltypeId.Value);
            var cover = _coverService.GetCoverById(selection.CoverId.Value);
            var diamond = _diamondService.GetDiamondById(selection.DiamondId.Value);
            if (size == null || metaltype == null || cover == null || diamond == null)
            {
                return BadRequest("Invalid selection.");
            }
            var productName = cover.CoverName + " " + diamond.DiamondName;
            var product = new Product
            {
                ProductName = productName,
                UnitPrice = cover.UnitPrice + size.SizePrice + metaltype.MetaltypePrice,
                DiamondId = selection.DiamondId.Value,
                CoverId = selection.CoverId.Value,
                MetaltypeId = selection.MetaltypeId.Value,
                SizeId = selection.SizeId.Value,
                Pp = "Custom"
            };
            var producthaha = _productService.GetAllProducts().FirstOrDefault(c => c.MetaltypeId == selection.MetaltypeId && c.CoverId == selection.CoverId && c.DiamondId == selection.DiamondId && c.SizeId == selection.SizeId);
            if (producthaha != null)
            {
                product.ProductId = producthaha.ProductId;
                return Ok(product.ProductId);
            }
            _productService.AddProduct(product);

            return Ok(product.ProductId);
        }
    }
}

