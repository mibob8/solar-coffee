using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Product;
using SolarCoffee.Web.Serialization;

namespace SolarCoffee.Web.Controllers
{

    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> logger;
        private readonly IProductService productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            this.logger = logger;
            this.productService = productService;
        }

        [HttpGet("/api/product")]
        public ActionResult GetProduct()
        {
            this.logger.LogInformation("Getting all products");
            var products = productService.GetAllProducts();
            var productViewModel = products.Select(product=>ProductMapper.SerializeProductModel(product));
            return Ok(productViewModel);
        }
    }
}