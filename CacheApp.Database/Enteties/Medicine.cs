namespace CacheApp.Database.Enteties;

public class Medicine
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public MedicineSpecification? Specification { get; set; }

    public Guid? CompanyId { get; set; }
    public required long Version { get; set; }

    // navigation props
    public ICollection<Substance> Ingredients { get; set; } = new List<Substance>();
    public Company? Manufacturer { get; set; }
    public ICollection<Tag>? Tags { get; set; }
}
