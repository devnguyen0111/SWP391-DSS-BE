using Model.Models;

namespace Services.Products
{
    public interface ICoverInventoryService
    {
        void CreateInventoryForCover(int coverId, int quantity);
        IEnumerable<CoverInventory> GetAllInventories();
        IEnumerable<CoverInventory> GetInventoriesByCoverId(int coverId);
        CoverInventory GetInventory(int coverId, int sizeId, int metaltypeId);
        bool ReduceInventoryByOne(int coverId, int sizeId, int metaltypeId);
        void UpdateInventory(int coverId, int sizeId, int metaltypeId, int newQuantity);
        bool checkIfOutOfStock(int coverId, int sizeId, int metaltypeId);
    }
}