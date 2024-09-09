using Microsoft.EntityFrameworkCore;

namespace Estoque_Concecionaria.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Classification> Classifications { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
