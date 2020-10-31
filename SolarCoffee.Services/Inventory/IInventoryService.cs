using System.Collections.Generic;
using SolarCoffee.Data.Model;
using SolarCoffee.Services.Product;

namespace SolarCoffee.Services.Inventory
{
    public interface IInventoryService
    {
        List<ProductInventory> GetCurrentInventory();
        ServiceResponse<ProductInventory> UpdateUnitsAvalible(int id, int adjustment);
        public ProductInventory GetByProductId(int productId);
        public void CreateSnapshot();
        public List<ProductInventorySnapshot> GetSnapshotHistory(); 
    }
}