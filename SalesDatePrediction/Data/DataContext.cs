using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Models;
using System.Diagnostics.Metrics;

namespace SalesDatePrediction.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Orders> Orders { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Shippers> Shippers { get; set; }
        public DbSet<Products> Products { get; set; }

    }
}
