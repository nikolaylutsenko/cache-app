namespace CacheApp.Database;

using Enteties;
using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Tag> Tags { get; set; }
}
