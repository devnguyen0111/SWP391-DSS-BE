using DAO;
namespace Services.Utility
{
    public class SearchService : ISearchService
    {
        private readonly DIAMOND_DBContext _dbContext;
        private readonly CalculatorService calculatorService;

        public SearchService(DIAMOND_DBContext dbContext, CalculatorService calculatorService)
        {
            _dbContext = dbContext;
            this.calculatorService = calculatorService;
        }

        public List<object> Search(string name)
        {
            var simpleString = name.ToLower();
            if (simpleString != null)
            {
                simpleString = simpleString.Trim();
            }

            var products = _dbContext.Products.Where(p => p.ProductName.Contains(simpleString)).ToList();
            var diamonds = _dbContext.Diamonds.Where(d => d.DiamondName.Contains(simpleString)).ToList();

            List<object> searchResults = new List<object>();

            if (products.Count > 0)
            {
                foreach (var product in products)
                {
                    searchResults.Add(new
                    {
                        Type = "Product",
                        productId = product.ProductId,
                        productName = product.ProductName
                    });
                }
            }

            if (diamonds.Count > 0)
            {
                foreach (var diamond in diamonds)
                {
                    searchResults.Add(new
                    {
                        Type = "Diamond",
                        diamondId = diamond.DiamondId,
                        diamondName = diamond.DiamondName
                    });
                }
            }

            if (searchResults.Count == 0)
            {
                searchResults.Add(new
                {
                    Type = "NoMatch",
                    Message = "No matching product or diamond found."
                });
            }

            return searchResults;
        }

        public async Task<object> SearchProduct(string name)
        {
            var simpleString = name.ToLower();
            if (simpleString != null)
            {
                simpleString = simpleString.Trim();
            }

            var searchResults = new List<object>();

            var products = _dbContext.Products.Where(p => p.ProductName.Contains(simpleString)).ToList();

            if (products.Count > 0)
            {
                foreach (var product in products)
                {
                    decimal? price = await calculatorService.GetProductPriceAsync(product.ProductId);
                    var newReturn = new ProductReturn
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        ProductPrice = price
                    };

                    searchResults.Add(newReturn);
                }
            }

            if (searchResults.Count == 0)
            {
                searchResults.Add(new
                {
                    Message = "No matching product found."
                });
            }
            return searchResults;
        }

        public List<object> SearchDiamond(string name)
        {
            var simpleString = name.ToLower();
            if (simpleString != null)
            {
                simpleString = simpleString.Trim();
            }

            var searchResults = new List<object>();

            var diamonds = _dbContext.Diamonds.Where(d => d.DiamondName.Contains(simpleString)).ToList();

            if (diamonds.Count > 0)
            {
                foreach (var diamond in diamonds)
                {
                    var newReturn = new DiamondReturn
                    {
                        DiamondName = diamond.DiamondName,
                        CaratWeight = diamond.CaratWeight,
                        Color = diamond.Color,
                        Clarity = diamond.Clarity,
                        Cut = diamond.Cut,
                        Shape = diamond.Shape,
                        Price = calculatorService.GetDiamondPriceAsync(diamond.DiamondId).Result
                    };

                    searchResults.Add(newReturn);
                }
            }
            if (searchResults.Count == 0)
            {
                searchResults.Add(new
                {
                    Message = "No matching diamond found."
                });
            }
            return searchResults;
        }
    }

    public class ProductReturn
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public decimal? ProductPrice { get; set; }
    }

    public class DiamondReturn
    {
        public string? DiamondName { get; set; }

        public decimal CaratWeight { get; set; }

        public string? Color { get; set; }

        public string? Clarity { get; set; }

        public string? Cut { get; set; }

        public string? Shape { get; set; }

        public decimal Price { get; set; }
    }
}
    