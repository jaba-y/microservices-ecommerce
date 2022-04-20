using System.Threading.Tasks;
using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchProvider;

        public SearchController(ISearchService searchProvider)
        {
            this.searchProvider = searchProvider;
        }
        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm searchTerm)
        {
            var results = await searchProvider.SearchAsync(searchTerm.CustomerId);
            if (results.IsSuccess)
            {
                return Ok(results.SearchResults);
            }
            return NotFound();
        }
    }
}