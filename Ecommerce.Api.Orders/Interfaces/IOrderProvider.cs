using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Api.Orders.Models;

namespace Ecommerce.Api.Orders.Interfaces
{
    public interface IOrderProvider
    {
        Task<(bool IsSuccess, IEnumerable<OrderModel> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}