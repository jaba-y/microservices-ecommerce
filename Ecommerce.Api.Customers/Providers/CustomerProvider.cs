using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Api.Customers.Db;
using Ecommerce.Api.Customers.Interfaces;
using Ecommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Api.Customers.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly CustomerDbContext dbContext;
        private readonly ILogger<CustomerProvider> logger;
        private readonly IMapper mapper;
        public CustomerProvider(CustomerDbContext dbContext, ILogger<CustomerProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Customer.Any())
            {
                dbContext.Customer.Add(new CustomerItem() { Id = 1, Name = "John", Address = "address 1" });
                dbContext.Customer.Add(new CustomerItem() { Id = 2, Name = "Ann", Address = "address 2" });
                dbContext.Customer.Add(new CustomerItem() { Id = 3, Name = "Ben", Address = "address 3" });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var Customers = await dbContext.Customer.ToListAsync();
                if (Customers != null && Customers.Any())
                {
                    var result = mapper.Map<IEnumerable<CustomerItem>, IEnumerable<Customer>>(Customers);
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

        public async Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var Customer = await dbContext.Customer.FirstOrDefaultAsync(x => x.Id == id);
                if (Customer != null)
                {
                    var result = mapper.Map<CustomerItem, Customer>(Customer);
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