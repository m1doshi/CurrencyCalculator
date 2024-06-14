using CalculatorWebAPI.Entities;
using CalculatorWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CalculatorWebAPI.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Result> Results { get; set; }
    }
}
