using System.Data.Common;
using System.Collections.Generic; 

namespace SolarCoffee.Services.Product
{
    public interface IProductService
    {
        List<Data.Model.Product> GetAllProducts();
        Data.Model.Product GetProductById(int id);
        ServiceResponse<Data.Model.Product> CreateProduct(Data.Model.Product product);
        ServiceResponse<Data.Model.Product> ArchiveProduct(int id);
        
    }
}