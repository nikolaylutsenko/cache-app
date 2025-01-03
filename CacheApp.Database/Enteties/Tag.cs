namespace CacheApp.Database.Enteties;

public class Tag
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required long Version { get; set; }

    // navigation props
    public ICollection<Medicine> Medication { get; set; } = new List<Medicine>();
}
