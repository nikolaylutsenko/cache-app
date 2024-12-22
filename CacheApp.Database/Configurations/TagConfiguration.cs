﻿namespace CacheApp.Database.Configurations;

using Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tags");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).IsRequired();
        builder.Property(t => t.Name).IsRequired().HasMaxLength(300);
        builder.Property(t => t.Version).IsRequired();

        builder.HasIndex(t => t.Name).IsUnique();
    }
}