using Microsoft.EntityFrameworkCore;
using VehiclesAPI.Models;

namespace VehiclesAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
