namespace Medicine.Database.Enteties;

public class Company : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Country { get; set; }
    public string? Address { get; set; }
    public string? ContactInfo { get; set; }
    public required long Version { get; set; }

    // navigation props
    public List<Substance> Substances { get; set; } = [];
}
