namespace CacheApp.Database.Configurations;

using Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).IsRequired();
        builder.Property(t => t.Name).HasMaxLength(300).IsRequired();
        builder.Property(t => t.Country).HasMaxLength(100).IsRequired(false);
        builder.Property(t => t.Address).HasMaxLength(500).IsRequired(false);
        builder.Property(t => t.ContactInfo).HasMaxLength(300).IsRequired(false);
        builder.Property(t => t.Version).IsRequired().IsConcurrencyToken();

        builder.HasIndex(t => t.Name).IsUnique();
    }
}
