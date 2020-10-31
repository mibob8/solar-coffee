using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using SolarCoffee.Data;
using SolarCoffee.Data.Model;
using System;

namespace SolarCoffee.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly SolarDbContext dbContext;

        public ProductService(SolarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ServiceResponse<Data.Model.Product> ArchiveProduct(int id)
        {
            try
            {
                var product = dbContext.Products.Find(id);
                product.IsArchived = true;
                dbContext.SaveChanges();

                return new ServiceResponse<Data.Model.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Archiver Product",
                    isSuccess = true,
                };
            }
            catch (System.Exception e)
            {
                return new ServiceResponse<Data.Model.Product>
                {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = e.StackTrace,
                    isSuccess = false,
                };
            }
        }

        public ServiceResponse<Data.Model.Product> CreateProduct(Data.Model.Product product)
        {
            try
            {
                dbContext.Products.AddAsync(product);

                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10,
                };

                dbContext.ProductInventories.AddAsync(newInventory);
                dbContext.SaveChangesAsync();

                return new ServiceResponse<Data.Model.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Saved new product",
                    isSuccess = true,
                };
            }
            catch (System.Exception)
            {
                return new ServiceResponse<Data.Model.Product>
                {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = "Error while saving new product",
                    isSuccess = false,
                };
            }
        }

        public List<Data.Model.Product> GetAllProducts()
        {
            return dbContext.Products.ToList();
        }

        public Data.Model.Product GetProductById(int id)
        {
            return dbContext.Products.Find(id);
        }
    }
}