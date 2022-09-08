using Microsoft.EntityFrameworkCore;

namespace CrudOperations.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Location> Locations { get; set; }
    }
}
