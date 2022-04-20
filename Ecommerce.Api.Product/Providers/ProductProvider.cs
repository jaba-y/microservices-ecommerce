using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Api.Product.Db;
using Ecommerce.Api.Product.Interfaces;
using Ecommerce.Api.Product.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Api.Product.Providers
{
    public class ProductProvider : IProductProvider
    {
        private readonly ProductDbContext dbContext;
        private readonly ILogger<ProductProvider> logger;
        private readonly IMapper mapper;
        public ProductProvider(ProductDbContext dbContext, ILogger<ProductProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Product.Any())
            {
                dbContext.Product.Add(new ProductItem() { Id = 1, Name = "Pen", Price = 10, Inventory = 10 });
                dbContext.Product.Add(new ProductItem() { Id = 2, Name = "Paper", Price = 10, Inventory = 14 });
                dbContext.Product.Add(new ProductItem() { Id = 3, Name = "Pencil", Price = 10, Inventory = 15 });
                dbContext.Product.Add(new ProductItem() { Id = 4, Name = "Bag", Price = 10, Inventory = 20 });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Products> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Product.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<ProductItem>, IEnumerable<Products>>(products);
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

        public async Task<(bool IsSuccess, Products Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await dbContext.Product.FirstOrDefaultAsync(x => x.Id == id);
                if (product != null)
                {
                    var result = mapper.Map<ProductItem, Products>(product);
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