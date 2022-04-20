using System.Threading.Tasks;
using Ecommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/Customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerProvider CustomerProvider;
        public CustomersController(ICustomerProvider CustomerProvider)
        {
            this.CustomerProvider = CustomerProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await CustomerProvider.GetCustomersAsync();
            if (result.IsSuccess)
                return Ok(result.Customers);
            return NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await CustomerProvider.GetCustomerAsync(id);
            if (result.IsSuccess)
                return Ok(result.Customer);
            return NotFound();
        }
    }
}