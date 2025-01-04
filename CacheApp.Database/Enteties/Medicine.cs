namespace CacheApp.Database.Enteties;

public class Medicine
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public Guid? ManufacturerId { get; set; }
    public required long Version { get; set; }

    // navigation props
    public List<Ingredient> Ingredients { get; set; } = [];
    public List<Substance> Substances { get; set; } = [];
    public Company? Manufacturer { get; set; }
    public List<Tag>? Tags { get; set; }
    public MedicineSpecification? Specification { get; set; }
}
