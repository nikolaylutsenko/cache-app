namespace CacheApp.Database.Enteties;

public class Medicine
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public Guid? ManufacturerId { get; set; }
    public required long Version { get; set; }

    // navigation props
    public ICollection<Ingridient> Ingredients { get; set; } = [];
    public ICollection<Substance> Substances { get; set; } = [];
    public Company? Manufacturer { get; set; }
    public ICollection<Tag>? Tags { get; set; }
    public MedicineSpecification? Specification { get; set; }
}

public class Ingridient
{
    public required Guid Id { get; set; }
    public required Guid MedicineId { get; set; }
    public required Guid SubstanceId { get; set; }
    public required decimal Quantity { get; set; }
    public required bool IsActive { get; set; }

    // navigation props
    public required Medicine Medicine { get; set; }
    public required Substance Substance { get; set; }
}
