namespace Medicine.Database;

using Enteties;
using Medicine.Database.Configurations;
using Microsoft.EntityFrameworkCore;

public class MedicineDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Substance> Substances { get; set; }
    public DbSet<Medicine> Medication { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TagConfiguration).Assembly);
    }
}
