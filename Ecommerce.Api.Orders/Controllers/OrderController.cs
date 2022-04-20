using System.Threading.Tasks;
using Ecommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProvider OrderProvider;
        public OrdersController(IOrderProvider OrderProvider)
        {
            this.OrderProvider = OrderProvider;
        }
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrderAsync(int customerId)
        {
            var result = await OrderProvider.GetOrdersAsync(customerId);
            if (result.IsSuccess)
                return Ok(result.Orders);
            return NotFound();
        }
    }
}