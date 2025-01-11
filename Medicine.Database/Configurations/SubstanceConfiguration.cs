namespace Medicine.Database.Configurations;

using Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SubstanceConfiguration : IEntityTypeConfiguration<Substance>
{
    public void Configure(EntityTypeBuilder<Substance> builder)
    {
        builder.ToTable(nameof(Substance));

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).IsRequired();
        builder.Property(t => t.Name).HasMaxLength(300).IsRequired();
        builder.Property(t => t.Description).HasMaxLength(1000).IsRequired(false);
        builder.Property(t => t.Formula).HasMaxLength(1000).IsRequired(false);
        builder.Property(t => t.Version).IsRequired().IsConcurrencyToken();

        builder.HasIndex(t => t.Name).IsUnique();

        builder
            .HasOne(t => t.Manufacturer)
            .WithMany(t => t.Substances)
            .HasForeignKey(t => t.ManufacturerId)
            .IsRequired(false);
    }
}
