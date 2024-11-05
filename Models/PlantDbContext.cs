using Microsoft.EntityFrameworkCore;

namespace WebPlantApi.Models
{
    public class PlantDbContext : DbContext
    {
        public PlantDbContext(DbContextOptions<PlantDbContext> options) : base(options) { }

        public DbSet<PlantIterms> PlantItems { get; set; }
    }
}
