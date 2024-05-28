using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;

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


        private async Task<decimal> CalculateCostPrice(int productId)
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
            decimal laborCost = 100; // tính labar cost sau || đang tạm để cững 100$

            return diamondCost + coverCost + laborCost;
        }

        private decimal GetCoverPrice(int coverId)
        {
            var cover = _context.Covers.FirstOrDefault(c => c.CoverId == coverId);
            return cover != null ? cover.UnitPrice : 0;
        }

        private decimal GetDiamondPrice(int diamondId)
        {
            var diamond = _context.Diamonds.FirstOrDefault(d => d.DiamondId == diamondId);
            return diamond != null ? diamond.Price : 0;
        }

        private decimal CalculateSellingPrice(decimal costPrice, string voucherName)
        {
            var voucher = _context.Vouchers.FirstOrDefault(v => v.Name == voucherName);
            var discountRate = voucher != null ? decimal.Parse(voucher.Description) : 1m;
            return costPrice * discountRate;
        }

        public async Task<decimal> CalculateProductPrice(int diamondId, int coverId, decimal laborCost, string voucherName)
        {
            decimal diamondPrice = GetDiamondPrice(diamondId);
            decimal coverPrice = GetCoverPrice(coverId);
            var productId = _context.Products.FirstOrDefault(p => p.DiamondId == diamondId && p.CoverId == coverId).ProductId;
            decimal costPrice = await CalculateCostPrice(productId);
            return CalculateSellingPrice(costPrice, voucherName);
        }

    }
}
