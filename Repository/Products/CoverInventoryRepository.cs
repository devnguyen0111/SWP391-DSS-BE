using DAO;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Products
{
    public class CoverInventoryRepository : ICoverInventoryRepository
    {
        private readonly DIAMOND_DBContext _context;

        public CoverInventoryRepository(DIAMOND_DBContext context)
        {
            _context = context;
        }

        public CoverInventory Add(CoverInventory coverInventory)
        {
            _context.CoverInventories.Add(coverInventory);
            return coverInventory;
        }
        public CoverInventory Update(CoverInventory coverInventory)
        {
            _context.Entry(coverInventory).State = EntityState.Modified;
            _context.SaveChanges();
            return coverInventory;
        }

        public void Remove(CoverInventory coverInventory)
        {
            _context.CoverInventories.Remove(coverInventory);
        }

        public CoverInventory Get(int coverId, int sizeId, int metaltypeId)
        {
            return _context.CoverInventories
                .FirstOrDefault(ci => ci.CoverId == coverId && ci.SizeId == sizeId && ci.MetaltypeId == metaltypeId);
        }
        public IEnumerable<CoverInventory> GetAllById(int coverId, int sizeId, int metaltypeId)
        {
            return _context.CoverInventories.Where(c => c.CoverId == coverId);
        }
        public bool reduceByOne(int coverid, int sizeid, int metaltypeId)
        {
            CoverInventory huh = Get(coverid, sizeid, metaltypeId);
            if (huh.Quantity == 0)
            {
                return false;
            }
            huh.Quantity -= 1;
            Update(huh);
            return true;
        }
        public IEnumerable<CoverInventory> GetAll()
        {
            return _context.CoverInventories.ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void CreateInventoryForCover(int coverId, int quantity)
        {
            // Get all sizes for the given cover
            var coverSizes = _context.CoverSizes.Where(cs => cs.CoverId == coverId).ToList();

            // Get all metal types for the given cover
            var coverMetaltypes = _context.CoverMetaltypes.Where(cm => cm.CoverId == coverId).ToList();

            // Create inventory for each combination of size and metal type
            foreach (var size in coverSizes)
            {
                foreach (var metaltype in coverMetaltypes)
                {
                    var existingInventory = _context.CoverInventories
                        .FirstOrDefault(ci => ci.CoverId == coverId && ci.SizeId == size.SizeId && ci.MetaltypeId == metaltype.MetaltypeId);

                    if (existingInventory == null)
                    {
                        _context.CoverInventories.Add(new CoverInventory
                        {
                            CoverId = coverId,
                            SizeId = size.SizeId,
                            MetaltypeId = metaltype.MetaltypeId,
                            Quantity = quantity,
                        });
                    }
                }
            }

            // Save changes to the database
            _context.SaveChanges();
        }
    }
}
