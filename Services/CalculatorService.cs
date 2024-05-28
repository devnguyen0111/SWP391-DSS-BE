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

        public async Task<decimal> CalculateCostPrice(int productId)
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
        }


    }
}
