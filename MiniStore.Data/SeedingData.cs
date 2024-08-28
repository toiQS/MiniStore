using Microsoft.EntityFrameworkCore;
using MiniStore.Models;

namespace MiniStore.Data
{
    public class SeedingData : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=AKAI;Database=Noname;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;User Id = akai;Password=Akai123");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
