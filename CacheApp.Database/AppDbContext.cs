namespace CacheApp.Database;

using Enteties;
using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // tips: if don't add to AppDbContest migrations will not wee entity
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Substance> Substances { get; set; }
}
