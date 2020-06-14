using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CartService
{
    public class CartDbContext : DbContext
    {
        public CartDbContext(DbContextOptions<CartDbContext> options)
            : base(options)
        { }
        public DbSet<OrderItem> OrderItems { get; set; }

    }
}