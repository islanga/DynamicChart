using Microsoft.EntityFrameworkCore;
using DynamicChartAPI.Models;

namespace DynamicChartAPI.Data
{
    public class DynamicChartDbContext : DbContext
    {
        public DynamicChartDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Sales> sales { get; set; }
    }
}
