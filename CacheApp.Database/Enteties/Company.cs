namespace CacheApp.Database.Enteties;

public class Company
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Country { get; set; }
    public required string Address { get; set; }
    public required string ContactInfo { get; set; }
}
