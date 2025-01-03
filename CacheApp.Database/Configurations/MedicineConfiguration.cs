namespace CacheApp.Database.Configurations;

using Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> builder)
    {
        builder.ToTable("Medication");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).IsRequired();
        builder.Property(t => t.Name).HasMaxLength(300).IsRequired();
        builder.Property(t => t.Description).HasMaxLength(1000).IsRequired(false);

        builder.HasIndex(t => t.Name).IsUnique();

        builder
            .HasMany(t => t.Tags)
            .WithMany(t => t.Medication)
            .UsingEntity(
                "Medication_Tags",
                r => r.HasOne(typeof(Medicine)).WithMany().HasForeignKey("MedicineId"),
                l => l.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagId")
            );
        builder
            .HasMany(t => t.Substances)
            .WithMany(t => t.Medication)
            .UsingEntity<Ingridient>("Ingridients");
        builder
            .HasOne(t => t.Specification)
            .WithOne(t => t.Medicine)
            .HasForeignKey<MedicineSpecification>(t => t.MedicineId)
            .IsRequired(false);
    }
}
