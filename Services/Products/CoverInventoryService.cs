using Model.Models;
using Repository.Products;
using Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Products
{
    public class CoverInventoryService : ICoverInventoryService
    {
        private readonly ICoverInventoryRepository _coverInventoryRepository;
        private readonly IDisableService _disableService;

        public CoverInventoryService(ICoverInventoryRepository coverInventoryRepository, IDisableService disableService)
        {
            _coverInventoryRepository = coverInventoryRepository;
            _disableService = disableService;
        }
        public void CreateInventoryForCover(int coverId, int quantity)
        {
            _coverInventoryRepository.CreateInventoryForCover(coverId, quantity);
        }
        public void UpdateInventory(int coverId, int sizeId, int metaltypeId, int newQuantity)
        {
            var inventory = _coverInventoryRepository.Get(coverId, sizeId, metaltypeId);
            if (inventory != null)
            {
                inventory.Quantity = newQuantity;
                _coverInventoryRepository.Update(inventory);
            }
            else
            {
                // Handle case where inventory record does not exist
                throw new KeyNotFoundException("Inventory record not found.");
            }
        }
        public bool ReduceInventoryByOne(int coverId, int sizeId, int metaltypeId)
        {
            return _coverInventoryRepository.reduceByOne(coverId, sizeId, metaltypeId);
        }
        public CoverInventory GetInventory(int coverId, int sizeId, int metaltypeId)
        {
            return _coverInventoryRepository.Get(coverId, sizeId, metaltypeId);
        }
        public bool checkIfOutOfStock(int coverId,int sizeId,int metaltypeId)
        {
            return _coverInventoryRepository.Get(coverId, sizeId, metaltypeId).Quantity == 0;
        }
        public IEnumerable<CoverInventory> GetAllInventories()
        {
            return _coverInventoryRepository.GetAll();
        }

        public IEnumerable<CoverInventory> GetInventoriesByCoverId(int coverId)
        {
            return _coverInventoryRepository.GetAllById(coverId);
        }
    }
}
