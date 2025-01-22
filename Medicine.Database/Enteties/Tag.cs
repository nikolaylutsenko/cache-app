namespace Medicine.Database.Enteties;

public class Tag : IDatabaseEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required long Version { get; set; }

    // navigation props
    public List<Medicine> Medicines { get; set; } = [];
}
