using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Utility
{
    public class StatusRepository : IStatusRepository
    {
        private readonly DIAMOND_DBContext _context;

        public StatusRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }
        public (bool CanChange, string Reason) CanChangeProductStatus(Product product)
        {
            if (IsProductComponentsAvailable(product))
            {
                return (true, "Product can be set to Available.");
            }
            else
            {
                return (false, "Either the Cover or Diamond or one of their components is Disabled.");
            }
        }

        private bool IsProductComponentsAvailable(Product product)
        {
            var cover = _context.Covers
                .Include(c => c.CoverMetaltypes)
                .Include(c => c.CoverSizes)
                .FirstOrDefault(c => c.CoverId == product.CoverId);

            if (cover == null || cover.Status == "Disabled")
            {
                return false;
            }

            var diamond = _context.Diamonds.FirstOrDefault(d => d.DiamondId == product.DiamondId);

            if (diamond == null || diamond.Status == "Disabled")
            {
                return false;
            }

            if (!cover.CoverMetaltypes.Any(cm => cm.Status == "Available"))
            {
                return false;
            }

            if (!cover.CoverSizes.Any(cs => cs.Status == "Available"))
            {
                return false;
            }

            return true;
        }

        public (bool CanChange, string Reason) CanChangeCoverStatus(Cover cover)
        {
            Cover checker = _context.Covers.Include(c => c.CoverMetaltypes).Include(c => c.CoverSizes).Where(c => c.CoverId==cover.CoverId).FirstOrDefault();
            if (cover.CoverSizes.Any(cs => cs.Status == "Available") &&
                cover.CoverMetaltypes.Any(cm => cm.Status == "Available"))
            {
                return (true, "Cover can be set to Available.");
            }
            else
            {
                return (false, "Either some CoverSizes or CoverMetaltypes are Disabled.");
            }
        }
        //public (bool CanChange, string Reason) CanChangeCoverStatus(Cover cover)
        //{
        //    if (cover.CoverSizes.All(cs => cs.Status == "Disabled") ||
        //        cover.CoverMetaltypes.All(cm => cm.Status == "Disabled"))
        //    {
        //        return (false, "Either some CoverSizes or CoverMetaltypes are Disabled.");
        //    }
        //    else
        //    {
        //        return (true, "Cover can be set to Available.");

        //    }
        //}
        //check if ... can be set to available
        public (bool CanChange, string Reason) CanChangeCoverSizeStatus(int coverId, int sizeId)
        {
            var coverSize = _context.CoverSizes.FirstOrDefault(cs => cs.CoverId == coverId && cs.SizeId == sizeId);
            if (coverSize == null)
            {
                return (false, "CoverSize not found.");
            }

            var size = _context.Sizes.FirstOrDefault(s => s.SizeId == sizeId);
            if (size == null || size.Status == "Disabled")
            {
                return (false, "Associated Size is Disabled.");
            }

            return (true, "CoverSize can be set to Available.");
        }
        //check if ... can be set to available
        public (bool CanChange, string Reason) CanChangeCoverMetalTypeStatus(int coverId, int metalTypeId)
        {
            var coverMetalType = _context.CoverMetaltypes.FirstOrDefault(cm => cm.CoverId == coverId && cm.MetaltypeId == metalTypeId);
            if (coverMetalType == null)
            {
                return (false, "CoverMetalType not found.");
            }

            var metaltype = _context.Metaltypes.FirstOrDefault(mt => mt.MetaltypeId == metalTypeId);
            if (metaltype == null || metaltype.Status == "Disabled")
            {
                return (false, "Associated Metaltype is Disabled.");
            }

            return (true, "CoverMetalType can be set to Available.");
        }

        public (bool CanChange, string Reason) CanChangeSizeStatus(int sizeId)
        {
            var size = _context.Sizes.FirstOrDefault(s => s.SizeId == sizeId);
            if (size == null)
            {
                return (false, "Size not found.");
            }

            if (size.Status == "Disabled")
            {
                return (false, "Size is Disabled.");
            }

            return (true, "Size can be set to Available.");
        }

        public (bool CanChange, string Reason) CanChangeMetalTypeStatus(int metalTypeId)
        {
            var metaltype = _context.Metaltypes.FirstOrDefault(mt => mt.MetaltypeId == metalTypeId);
            if (metaltype == null)
            {
                return (false, "Metaltype not found.");
            }

            if (metaltype.Status == "Disabled")
            {
                return (false, "Metaltype is Disabled.");
            }

            return (true, "Metaltype can be set to Available.");
        }

        public void UpdateProductStatus(Product product)
        {
            var (canChange, reason) = CanChangeProductStatus(product);
            if (canChange && product.Status == "Disabled")
            {
                product.Status = "Available";
            }
            else if (!canChange || product.Status == "Available")
            {
                product.Status = "Disabled";
            }
            _context.SaveChanges();
        }

        public void UpdateCoverStatus(Cover cover)
        {
            var (canChange, reason) = CanChangeCoverStatus(cover);

            // Set the status based on canChange and the current status
            if (canChange && cover.Status == "Disabled")
            {
                cover.Status = "Available";
            }
            else if (!canChange || cover.Status == "Available")
            {
                cover.Status = "Disabled";
            }
            _context.SaveChanges();

            // Update statuses of products that use this cover
            var products = _context.Products.Where(p => p.CoverId == cover.CoverId).ToList();
            foreach (var product in products)
            {
                UpdateProductStatus(product);
            }
        }
        public void UpdateCoverStatus1(Cover cover)
        {
            var (canChange, reason) = CanChangeCoverStatus(cover);

            // Set the status based on canChange and the current status
            if (canChange )
            {
                cover.Status = "Available";
            }
            else if (!canChange)
            {
                cover.Status = "Disabled";
            }
            _context.SaveChanges();

            // Update statuses of products that use this cover
            var products = _context.Products.Where(p => p.CoverId == cover.CoverId).ToList();
            foreach (var product in products)
            {
                UpdateProductStatus(product);
            }
        }

        public void UpdateCoverSizeStatus(int coverId, int sizeId, string newStatus)
        {
            // Validate the new status
            if (newStatus != "Available" && newStatus != "Disabled")
            {
                throw new ArgumentException("Invalid status value. Only 'Available' or 'Disabled' are allowed.");
            }

            var coverSize = _context.CoverSizes.FirstOrDefault(cs => cs.CoverId == coverId && cs.SizeId == sizeId);
            if (coverSize == null) return;

            // Update the cover size's status
            coverSize.Status = newStatus;
            _context.SaveChanges(); // Save changes to the database

            // Retrieve the cover that uses this cover size
            var cover = _context.Covers
                .Include(c => c.CoverSizes)
                .Include(c => c.CoverMetaltypes)
                .FirstOrDefault(c => c.CoverId == coverId);

            // If the cover is found, update its status
            if (cover != null)
            {
                UpdateCoverStatus(cover);
            }
        }
        public void UpdateCoverMetalTypeStatus(int coverId, int metalTypeId, string newStatus)
        {
            // Validate the new status
            if (newStatus != "Available" && newStatus != "Disabled")
            {
                throw new ArgumentException("Invalid status value. Only 'Available' or 'Disabled' are allowed.");
            }

            var coverMetalType = _context.CoverMetaltypes.FirstOrDefault(cm => cm.CoverId == coverId && cm.MetaltypeId == metalTypeId);
            if (coverMetalType == null) return;

            // Update the cover metal type's status
            coverMetalType.Status = newStatus;
            _context.SaveChanges(); // Save changes to the database

            // Retrieve the cover that uses this cover metal type
            var cover = _context.Covers
                .Include(c => c.CoverMetaltypes)
                .FirstOrDefault(c => c.CoverId == coverId);

            // If the cover is found, update its status
            if (cover != null)
            {
                UpdateCoverStatus(cover);
            }
        }


        public void UpdateSizeStatus(int sizeId, string newStatus)
        {
            // Validate the new status
            if (newStatus != "Available" && newStatus != "Disabled")
            {
                throw new ArgumentException("Invalid status value. Only 'Available' or 'Disabled' are allowed.");
            }

            // Retrieve the size from the database
            var size = _context.Sizes.FirstOrDefault(s => s.SizeId == sizeId);
            if (size == null) return; // If the size is not found, return early

            // Update the size's status
            size.Status = newStatus;
            _context.SaveChanges(); // Save changes to the database

            // Retrieve all cover sizes associated with this size
            var coverSizes = _context.CoverSizes.Where(cs => cs.SizeId == sizeId).ToList();

            // Update the status of each associated cover size
            foreach (var coverSize in coverSizes)
            {
                coverSize.Status = newStatus;
                _context.SaveChanges(); // Save changes to the database

                // Retrieve the cover that uses this cover size
                var cover = _context.Covers
                    .Include(c => c.CoverSizes)
                    .FirstOrDefault(c => c.CoverId == coverSize.CoverId);

                // If the cover is found, update its status
                if (cover != null)
                {
                    UpdateCoverStatus(cover);
                }
            }
        }

        public void UpdateMetalTypeStatus(int metalTypeId, string newStatus)
        {
            // Validate the new status
            if (newStatus != "Available" && newStatus != "Disabled")
            {
                throw new ArgumentException("Invalid status value. Only 'Available' or 'Disabled' are allowed.");
            }

            // Retrieve the metal type from the database
            var metaltype = _context.Metaltypes.FirstOrDefault(mt => mt.MetaltypeId == metalTypeId);
            if (metaltype == null) return; // If the metal type is not found, return early

            // Update the metal type's status
            metaltype.Status = newStatus;
            _context.SaveChanges(); // Save changes to the database

            // Retrieve all cover metal types associated with this metal type
            var coverMetalTypes = _context.CoverMetaltypes.Where(cm => cm.MetaltypeId == metalTypeId).ToList();

            // Update the status of each associated cover metal type
            foreach (var coverMetalType in coverMetalTypes)
            {
                coverMetalType.Status = newStatus;
                _context.SaveChanges(); // Save changes to the database

                // Retrieve the cover that uses this cover metal type
                var cover = _context.Covers
                    .Include(c => c.CoverMetaltypes)
                    .FirstOrDefault(c => c.CoverId == coverMetalType.CoverId);

                // If the cover is found, update its status
                if (cover != null)
                {
                    UpdateCoverStatus(cover);
                }
            }
        }
        public string DetermineCoverStatus(Cover cover)
        {
            if (!cover.CoverSizes.Any(cs => cs.Status == "Available") ||
                !cover.CoverMetaltypes.Any(cm => cm.Status == "Available"))
            {
                return "Disabled";
            }
            else
            {
                return "Available";
            }
        }
        public string DetermineProductStatus(Product product)
        {
            if (IsProductComponentsAvailable(product))
            {
                return "Available";
            }
            else
            {
                return "Disabled";
            }
        }
        public string DetermineCoverSizeStatus(int coverId, int sizeId)
        {
            var coverSize = _context.CoverSizes.FirstOrDefault(cs => cs.CoverId == coverId && cs.SizeId == sizeId);
            if (coverSize == null) return "Disabled";

            var size = _context.Sizes.FirstOrDefault(s => s.SizeId == coverSize.SizeId);
            if (size == null || size.Status == "Disabled")
            {
                return "Disabled";
            }
            return coverSize.Status;
        }

        public string DetermineCoverMetalTypeStatus(int coverId, int metalTypeId)
        {
            var coverMetalType = _context.CoverMetaltypes.FirstOrDefault(cm => cm.CoverId == coverId && cm.MetaltypeId == metalTypeId);
            if (coverMetalType == null) return "Disabled";

            var metaltype = _context.Metaltypes.FirstOrDefault(mt => mt.MetaltypeId == coverMetalType.MetaltypeId);
            if (metaltype == null || metaltype.Status == "Disabled")
            {
                return "Disabled";
            }
            return coverMetalType.Status;
        }

        public string DetermineSizeStatus(int sizeId)
        {
            var size = _context.Sizes.FirstOrDefault(s => s.SizeId == sizeId);
            if (size == null || size.Status == "Disabled")
            {
                return "Disabled";
            }
            return "Available";
        }
        public string DetermineMetalTypeStatus(int metalTypeId)
        {
            var metaltype = _context.Metaltypes.FirstOrDefault(mt => mt.MetaltypeId == metalTypeId);
            if (metaltype == null || metaltype.Status == "Disabled")
            {
                return "Disabled";
            }
            return "Available";
        }
        public void UpdateDiamondStatus(int diamondId, string newStatus)
        {
            var diamond = _context.Diamonds.FirstOrDefault(d => d.DiamondId == diamondId);
            if (diamond == null) return;

            // Validate the new status
            if (newStatus != "Available" && newStatus != "Disabled")
            {
                throw new ArgumentException("Invalid status value. Only 'Available' or 'Disabled' are allowed.");
            }

            // Update the diamond's status
            diamond.Status = newStatus;
            _context.SaveChanges();

            // Update statuses of products that use this diamond
            var products = _context.Products.Where(p => p.DiamondId == diamond.DiamondId).ToList();
            foreach (var product in products)
            {
                UpdateProductStatus(product);
            }
        }


        // Check if the diamond status can be changed
        public (bool CanChange, string Reason) CanChangeDiamondStatus(Diamond diamond)
        {
            bool huh = _context.ProductOrders
                .Any(po => po.Product.DiamondId == diamond.DiamondId);
            //po.Order.Status != "Cancel"
            //
            if (huh)
            {
                return (!huh, "Diamond is already bought");
            }
            else
            {
                return (huh, "You can change this diamond");
            }
        }
    }
}
