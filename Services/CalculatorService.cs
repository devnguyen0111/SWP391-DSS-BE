using DAO;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class CalculatorService
    {
        //  << Giá bán = giá vốn sản phẩm * tỉ lệ áp giá, Giá vốn sản phẩm = tiền kim cương + vỏ kim cương + tiền công>>

        private readonly DIAMOND_DBContext _context;

        public CalculatorService(DIAMOND_DBContext context)
        {
            this._context = context;
        }

        /*        public async Task<decimal> CalculateCostPrice(int productId)
                {
                    var product = await _context.Products
                        .Include(p => p.Diamond)
                        .Include(p => p.Cover)
                        .FirstOrDefaultAsync(p => p.ProductId == productId);

                    if (product == null)
                    {
                        throw new Exception("Product not found");
                    }

                    decimal diamondCost = product.Diamond.Price;
                    decimal coverCost = product.Cover.UnitPrice;
                    decimal laborCost = 0; // tính labar cost sau

                    return diamondCost + coverCost + laborCost;
                }

                public async Task<decimal> CalculateSellingPrice(int productId, decimal priceMarkupRate)
                {
                    decimal costPrice = await CalculateCostPrice(productId);
                    return costPrice * priceMarkupRate;
                }*/

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
                return Result=0;
            }
            var voucher = _context.Vouchers.FirstOrDefault(v => v.Name == Voucher);
            if (voucher == null)
            {
                Console.WriteLine("Voucher not found");
                return Result=0;

            }
            else if (
                voucher.ExpDate < DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
            {
                Console.WriteLine("Voucher expired");
                return Result=2;
            }
            else if (voucher.CusId == null)
            {
                Console.WriteLine("Voucher not belong to any customer");
                return Result=3;
            }
            return Result=1;
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
                    if (CheckVoucherValid(Voucher)== 1)
                    {

                    
                    var voucherDate = await _context.Vouchers.Where(v => v.Name == Voucher).Select(v => v.ExpDate).FirstOrDefaultAsync();
                    if (voucherDate < DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
                    {
                        Console.WriteLine("Voucher: " + voucher.Name + "Voucher Date: " + voucherDate + " -> Expired");
                        return costPrice;

                        throw new Exception("Voucher expired");
                    }

                    decimal voucherRate = await _context.Vouchers.Where(v => v.Name == Voucher).Select(v => v.rate).FirstOrDefaultAsync();
                    Console.WriteLine("Voucher rate: " + voucherRate);
                    //convert 10 to 10%
                    voucherRate = voucherRate / 100;
                    Console.WriteLine("Voucher rate after convert: " + voucherRate);
                    //costPrice after discount
                    costPrice = costPrice - (costPrice * voucherRate);
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
    }
}
