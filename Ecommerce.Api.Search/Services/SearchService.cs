using System.Threading.Tasks;
using Ecommerce.Api.Search.Interfaces;

namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;

        public SearchService(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var result = await orderService.GetOrdersAsync(customerId);
            if (result.IsSuccess)
            {
                return (true,  new { Orders = result });
            }
            return (true, new { Message = "Hello" });
        }
    }
}