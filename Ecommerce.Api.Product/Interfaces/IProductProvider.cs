using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Api.Product.Models;

namespace Ecommerce.Api.Product.Interfaces
{
    public interface IProductProvider
    {
        Task<(bool IsSuccess, IEnumerable<Products> Products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, Products Product, string ErrorMessage)> GetProductAsync(int id);

    }
}