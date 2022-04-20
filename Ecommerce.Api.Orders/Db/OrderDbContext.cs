
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Orders.Db
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public OrderDbContext(DbContextOptions options) : base(options)
        { }
    }
}