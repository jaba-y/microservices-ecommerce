using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Api.Product.Db;
using Ecommerce.Api.Product.Profiles;
using Ecommerce.Api.Product.Providers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ecommerce.Api.Product.Test
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>().UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts)).Options;
            //creating dbContext
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);
            //creating mapper for provider
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(c => c.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            //creating provider 
            var productProvider = new ProductProvider(dbContext, null, mapper);

            var products =await productProvider.GetProductsAsync();
            Assert.False(products.IsSuccess);
            Assert.True(products.Products.Any());
            Assert.Null(products.ErrorMessage);
        }

        private void CreateProducts(ProductDbContext dbContext)
        {
            //as using in memory database
            for (int i = 0; i < 10; i++)
            {
                dbContext.Add(new ProductItem()
                {
                    Id = i,
                    Name = "Product" + i,
                    Price = i + 5,
                    Inventory = i * 2
                });
            }
            dbContext.SaveChanges();
        }
    }
}
