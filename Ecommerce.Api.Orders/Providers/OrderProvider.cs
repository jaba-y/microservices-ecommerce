using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Api.Orders.Db;
using Ecommerce.Api.Orders.Interfaces;
using Ecommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Api.Orders.Providers
{
    public class OrderProvider : IOrderProvider
    {
        private readonly OrderDbContext dbContext;
        private readonly ILogger<OrderProvider> logger;
        private readonly IMapper mapper;
        public OrderProvider(OrderDbContext dbContext, ILogger<OrderProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {

            if (!dbContext.Order.Any())
            {
                dbContext.Order.Add(new Order() { Id = 1, CustomerId = 1, OrderDate = DateTime.Now, Total = 10, Items = new List<OrderItem> { new OrderItem() { Id = 1, OrderId = 1, ProductId = 1, Quantity = 3, UnitPrice = 10 } } });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<OrderModel> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var Order = await dbContext.Order.Where(x => x.CustomerId == customerId).ToListAsync();
                if (Order != null && Order.Any())
                {
                    var result = mapper.Map<IEnumerable<Order>, IEnumerable<OrderModel>>(Order);
                    return (true, result, null);
                }
                return (false, null, "No Data");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}