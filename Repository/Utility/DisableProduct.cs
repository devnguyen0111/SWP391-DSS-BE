using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Utility
{
    public class DisableProduct
    {
        private readonly DIAMOND_DBContext _context;

        public DisableProduct(DIAMOND_DBContext context)
        {
            _context = context;
        }
        //The change in Status only reflected when customize an Product (premade products are uneffected)
        // ==> The change in Status of items, which are not Product will only effect themselves
        //Example: Change of MetalTypeid = 1 will effect all the cover with CoverMetalTypeId = 1 but not Product with CoverId = 1 and MetalTypeId = 1
        //All products with the status of "premade" have already been made beforehand and are therefore unaffected by changes in status.
        
        public bool DisableItem(string itemType, int itemId,int? coverId)
        {
            bool canDisable = false;

            switch (itemType)
            {
                case "Product":
                    canDisable = CanDisableProduct(itemId);
                    if (canDisable)
                    {
                        var product = _context.Products.Find(itemId);
                        if (product != null)
                        {
                            product.Status = "Disabled";
                        }
                    }
                    break;

                case "Size":
                    canDisable = CanDisableSize(itemId);
                    if (canDisable)
                    {
                        var size = _context.Sizes.Find(itemId);
                        if (size != null)
                        {
                            size.Status = "Disabled";
                        }
                    }
                    break;

                case "MetalType":
                    canDisable = CanDisableMetalType(itemId);
                    if (canDisable)
                    {
                        var metalType = _context.Metaltypes.Find(itemId);
                        if (metalType != null)
                        {
                            metalType.Status = "Disabled";
                        }
                    }
                    break;

                case "Diamond":
                    canDisable = CanDisableDiamond(itemId);
                    if (canDisable)
                    {
                        var diamond = _context.Diamonds.Find(itemId);
                        if (diamond != null)
                        {
                            diamond.Status = "Disabled";
                        }
                    }
                    break;

                case "CoverMetalType":
                    canDisable = CanDisableCoverMetalType(itemId, (int)coverId);
                    if (canDisable)
                    {
                        var coverMetalType = _context.CoverMetaltypes.Find(itemId);
                        if (coverMetalType != null)
                        {
                            coverMetalType.Status = "Disabled";
                        }
                    }
                    break;

                case "CoverSize":
                    canDisable = CanDisableCoverSize(itemId, (int)coverId);
                    if (canDisable)
                    {
                        var coverSize = _context.CoverSizes.Find(itemId);
                        if (coverSize != null)
                        {
                            coverSize.Status = "Disabled";
                        }
                    }
                    break;
            }

            if (canDisable)
            {
                _context.SaveChanges();
            }

            return canDisable;
        }
        //This method group is to check if a product (or non Product) is valid for disable action
        //via check if any of them exist in an incomplete order
        private bool CanDisableProduct(int productId)
        {
            return !_context.ProductOrders
                .Any(po => po.ProductId == productId && po.Order.Status == "Delivered");
        }

        private bool CanDisableSize(int sizeId)
        {
            return !_context.ProductOrders
                .Any(po => po.Product.SizeId == sizeId && po.Order.Status == "Delivered");
        }

        private bool CanDisableMetalType(int metalTypeId)
        {
            return !_context.ProductOrders
                .Any(po => po.Product.MetaltypeId == metalTypeId && po.Order.Status == "Delivered");
        }

        private bool CanDisableDiamond(int diamondId)
        {
            return !_context.ProductOrders
                .Any(po => po.Product.DiamondId == diamondId && po.Order.Status == "Delivered");
        }

        private bool CanDisableCoverMetalType(int coverMetalTypeId, int coverId)
        {
            return !_context.ProductOrders
                .Any(po => po.Product.CoverId == coverId &&
                           po.Product.Cover.CoverMetaltypes
                               .Any(cmt => cmt.MetaltypeId == coverMetalTypeId && cmt.CoverId == coverId) &&
                           po.Order.Status == "Delivered");
        }

        private bool CanDisableCoverSize(int coverSizeId, int coverId)
        {
            return !_context.ProductOrders
                .Any(po => po.Product.CoverId == coverId &&
                           po.Product.Cover.CoverSizes
                               .Any(cs => cs.SizeId == coverSizeId && cs.CoverId == coverId) &&
                           po.Order.Status == "Delivered");
        }
    }
}
