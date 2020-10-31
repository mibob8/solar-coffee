using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data;
using SolarCoffee.Data.Model;
using System.Linq;
using SolarCoffee.Services.Product;
using Microsoft.Extensions.Logging;

namespace SolarCoffee.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly SolarDbContext context;
        private readonly ILogger<InventoryService> logger;

        public InventoryService(SolarDbContext context, ILogger<InventoryService> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        private void CreateSnapshot(ProductInventory inventory)
        {
            var now = DateTime.UtcNow;

            var snapshot = new ProductInventorySnapshot
            {
                SnapshotTime = now,
                Product = inventory.Product,
                QuantityOnHand = inventory.QuantityOnHand,
            };

            context.Add(snapshot);
            context.SaveChanges();

        }

        public ProductInventory GetByProductId(int productId)
        {
            return context.ProductInventories
            .Include(pi => pi.Product)
            .FirstOrDefault(pi => pi.Product.Id == productId);
        }

        public List<ProductInventory> GetCurrentInventory()
        {
            return context.ProductInventories
            .Include(pi => pi.Product)
            .Where(inventory => !inventory.Product.IsArchived)
            .ToList();
        }

        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(6);

            return context.ProductInventorySnapshots
            .Include(snap=>snap.Product)
            .Where(
                snapshot=>snapshot.SnapshotTime > earliest
                && !snapshot.Product.IsArchived)
            .ToList();

        }

        public ServiceResponse<ProductInventory> UpdateUnitsAvalible(int id, int adjustment)
        {
            try
            {
                var inventory = context.ProductInventories
                .Include(inv => inv.Product)
                .First(inventory => inventory.Product.Id == id);

                inventory.QuantityOnHand += adjustment;

                try
                {
                    CreateSnapshot(inventory);
                }
                catch (System.Exception)
                {

                    logger.LogError("error");
                }

                context.SaveChanges();

                return new ServiceResponse<ProductInventory>
                {
                    isSuccess = true,
                    Data = inventory,
                    Message = $"Product {id} inventory adjusted",
                    Time = DateTime.Now,
                };
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}