namespace CacheApp.Database.Enteties;

public class Company
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Country { get; set; }
    public string? Address { get; set; }
    public string? ContactInfo { get; set; }
    public required long Version { get; set; }

    // navigation props
    public ICollection<Substance> Substances { get; set; } = new List<Substance>();
}
