using BlueModas.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlueModas.Repository
{
    public class BlueModasContext : DbContext
    {
        public BlueModasContext(DbContextOptions<BlueModasContext> options) : base(options)
        {
            
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}