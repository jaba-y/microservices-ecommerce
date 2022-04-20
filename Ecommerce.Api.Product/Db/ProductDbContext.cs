
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Product.Db
{
    public class ProductDbContext : DbContext
    {
        public DbSet<ProductItem> Product { get; set; }
        public ProductDbContext(DbContextOptions options) : base(options)
        { }
    }
}