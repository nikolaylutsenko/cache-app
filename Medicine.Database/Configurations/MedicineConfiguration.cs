namespace Medicine.Database.Configurations;

using Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> builder)
    {
        builder.ToTable(nameof(Medicine));
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).IsRequired();
        builder.Property(t => t.Name).HasMaxLength(300).IsRequired();
        builder.Property(t => t.Description).HasMaxLength(1000).IsRequired(false);

        builder.HasIndex(t => t.Name).IsUnique();

        builder
            .HasMany(t => t.Tags)
            .WithMany(t => t.Medication)
            .UsingEntity(
                "Medicine_Tags",
                l => l.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagId"),
                r => r.HasOne(typeof(Medicine)).WithMany().HasForeignKey("MedicineId"),
                j => j.HasKey("TagId", "MedicineId")
            );
        //TODO: fix many-to-many
        //builder
        //    .HasMany(t => t.Substances)
        //    .WithMany(t => t.Medication)
        //    .UsingEntity<Ingredient>(
        //        "Ingredient",
        //        l =>
        //            l.HasOne(t => t.Substance)
        //                .WithMany(t => t.Ingredients)
        //                .HasForeignKey(t => t.SubstanceId),
        //        r =>
        //            r.HasOne(t => t.Medicine)
        //                .WithMany(t => t.Ingredients)
        //                .HasForeignKey(t => t.MedicineId),
        //        j => j.HasKey(t => t.Id)
        //    );
        builder
            .HasOne(t => t.Specification)
            .WithOne(t => t.Medicine)
            .HasForeignKey<MedicineSpecification>(t => t.MedicineId)
            .IsRequired(false);
    }
}
