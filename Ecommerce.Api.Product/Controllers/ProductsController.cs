using System.Threading.Tasks;
using Ecommerce.Api.Product.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Product.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductProvider productProvider;
        public ProductsController(IProductProvider productProvider)
        {
            this.productProvider = productProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await productProvider.GetProductsAsync();
            if (result.IsSuccess)
                return Ok(result.Products);
            return NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await productProvider.GetProductAsync(id);
            if (result.IsSuccess)
                return Ok(result.Product);
            return NotFound();
        }
    }
}