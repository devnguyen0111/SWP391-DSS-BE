using DAO;
using Microsoft.EntityFrameworkCore;

namespace Services.Utility
{
    public class CalculatorService
    {
        //  << Giá bán = giá vốn sản phẩm * tỉ lệ áp giá, Giá vốn sản phẩm = tiền kim cương + vỏ kim cương + tiền công>>

        private readonly DIAMOND_DBContext _context;

        public CalculatorService(DIAMOND_DBContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetDiamondPriceAsync(int diamondId)
        {
            var diamond = await _context.Diamonds.FindAsync(diamondId);
            if (diamond == null)
            {
                throw new Exception("Diamond not found");
            }
            else
            {
                return diamond?.Price ?? 0;
            }
        }

        public async Task<decimal> GetCoverPriceAsync(int coverId)
        {
            var cover = await _context.Covers.FindAsync(coverId);
            if (cover == null)
            {
                throw new Exception("Cover not found");
            }
            else
            {
                return cover?.UnitPrice ?? 0;
            }
        }

        //get cusId and check if voucher is valid
        public async Task<int> GetCusIdByVoucherAsync(string Voucher)
        {
            if (Voucher == null)
            {
                return 0;
            }
            var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.Name == Voucher);
            if (voucher == null)
            {
                return 0;
                throw new Exception("Voucher not found");
            }
            else if (
                voucher.ExpDate < DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
            {
                return 0;
                throw new Exception("Voucher expired");
            }
            else if (voucher.CusId == null)
            {
                return 0;
                throw new Exception("Voucher not belong to any customer");
            }
            return voucher.CusId;
        }

        /*public bool CheckVoucherEXP(string Voucher)
        {
            if (Voucher == null)
            {
                return false;
            }

            var voucher = _context.Vouchers.FirstOrDefault(v => v.Name == Voucher);
            if (voucher == null)
            {
                return false;
                throw new Exception("Voucher not found");
            }
            else if (
                voucher.ExpDate < DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
            {
                return false;
                throw new Exception("Voucher expired");
            }
            return true;

        }*/

        public int CheckVoucherValid(string Voucher)
        {

            // 0 = Cút (đ tìm thấy mã) | 1 = có (giảm giá OK) | 2 = đéo(hết hạn) | 3 = biến(cusId=null)
            var Result = 0;
            if (Voucher == null)
            {
                return Result = 0;
            }
            var voucher = _context.Vouchers.FirstOrDefault(v => v.Name == Voucher);
            if (voucher == null)
            {
                Console.WriteLine("Voucher not found");
                return Result = 0;

            }
            else if (
                voucher.ExpDate < DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
            {
                Console.WriteLine("Voucher expired");
                return Result = 2;
            }
            else if (GetCusIdByVoucherAsync(Voucher).Result == 0)
            {
                Console.WriteLine("Voucher not belong to any customer");
                return Result = 3;
            }
            return Result = 1;
        }

        public async Task<decimal> ApplyVoucherAsync(string Voucher, decimal costPrice)
        {
            if (Voucher == null)
            {
                return costPrice;
            }
            try
            {
                var voucher = await _context.Vouchers.FirstOrDefaultAsync(v => v.Name == Voucher);
                Console.WriteLine("Voucher: " + voucher.Name);
                if (voucher == null)
                {
                    return costPrice;
                    throw new Exception("Voucher not found");
                }
                else
                {
                    if (CheckVoucherValid(Voucher) == 1)
                    {


                        var voucherDate = await _context.Vouchers.Where(v => v.Name == Voucher).Select(v => v.ExpDate).FirstOrDefaultAsync();
                        if (voucherDate < DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
                        {
                            Console.WriteLine("Voucher: " + voucher.Name + "Voucher Date: " + voucherDate + " -> Expired");
                            return costPrice;

                            throw new Exception("Voucher expired");
                        }

                        decimal voucherRate = await _context.Vouchers.Where(v => v.Name == Voucher).Select(v => v.Rate).FirstOrDefaultAsync();
                        Console.WriteLine("Voucher rate: " + voucherRate);
                        //convert 10 to 10%
                        voucherRate = voucherRate / 100;
                        Console.WriteLine("Voucher rate after convert: " + voucherRate);
                        //costPrice after discount
                        costPrice = costPrice - costPrice * voucherRate;
                        Console.WriteLine("Cost price after discount: " + costPrice);
                        return costPrice;
                    }
                    else
                    {
                        return costPrice;
                    }
                }

            }
            catch (Exception ex)
            {
                return costPrice;
                throw new Exception("Voucher not found");

            }
        }

        public async Task<decimal> CalculateCostPriceAsync(int diamondId, int coverId)
        {
            decimal diamondCost = await GetDiamondPriceAsync(diamondId);
            decimal coverCost = await GetCoverPriceAsync(coverId);
            int laborCost = 100; // labor cost = 100$
            return diamondCost + coverCost + laborCost;

        }

        public async Task<decimal> CalculateSellingPriceAsync(int diamondId, int coverId, string voucherName)
        {
            decimal costPrice = await CalculateCostPriceAsync(diamondId, coverId);
            costPrice = await ApplyVoucherAsync(voucherName, costPrice);
            Console.WriteLine("CalculateSellingPriceAsync CostPrice after discount: " + costPrice);
            decimal priceMarkupRate = 1.2m;
            Console.WriteLine("CalculateSellingPriceAsync Final Price: " + costPrice * priceMarkupRate);
            return costPrice * priceMarkupRate;
        }


        public async Task<decimal> GetProductPriceAsync(int productId)
        {
            var product = await _context.Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
            if (product != null)
            {
                //decimal? productPrice = product.UnitPrice;
                decimal? productMetalTypePrice = await _context.Metaltypes.Where(m => m.MetaltypeId == product.MetaltypeId).Select(m => m.MetaltypePrice).FirstOrDefaultAsync();
                int? productDiamondId = product.DiamondId;
                decimal diamondPrice = await GetDiamondPriceAsync((int)productDiamondId);
                int? productSizeId = product.SizeId;
                int? productCoverId = product.CoverId;
                decimal productCostPrice = await CalculateCostPriceAsync((int)productDiamondId, (int)productCoverId);
                decimal priceMarkupRate = 1.2m;
                decimal? productSizePrice = await _context.Sizes.Where(s => s.SizeId == product.SizeId).Select(s => s.SizePrice).FirstOrDefaultAsync();
                decimal? productPrice = (productCostPrice  + productSizePrice + product.UnitPrice) * priceMarkupRate;
                Console.WriteLine($"Product Final price: {productPrice} | productUnit: {product.UnitPrice} | diamond Price: {diamondPrice} | metalType Price: {productMetalTypePrice} | size Price: {productSizePrice}");
                return (decimal)productPrice;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
