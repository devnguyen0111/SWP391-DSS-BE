using Model.Models;

namespace Repository.Products
{
    public interface ICoverInventoryRepository
    {
        CoverInventory Add(CoverInventory coverInventory);
        void CreateInventoryForCover(int coverId, int quantity);
        CoverInventory Get(int coverId, int sizeId, int metaltypeId);
        IEnumerable<CoverInventory> GetAll();
        IEnumerable<CoverInventory> GetAllById(int coverId);
        bool reduceByOne(int coverid, int sizeid, int metaltypeId);
        void Remove(CoverInventory coverInventory);
        void SaveChanges();
        CoverInventory Update(CoverInventory coverInventory);
    }
}