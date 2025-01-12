namespace Medicine.Database.Enteties;

public class Substance : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Formula { get; set; }
    public Guid? ManufacturerId { get; set; }
    public long Version { get; set; }

    // navigation props
    public Company? Manufacturer { get; set; }
    public List<Medicine> Medicines { get; set; } = [];
    public List<Ingredient> Ingredients { get; set; } = [];
}
