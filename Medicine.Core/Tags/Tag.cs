using CacheApp.Utils.Aggregates;

namespace Medicine.Domain.Tags;

public class Tag(Guid Id, string name) : Entity(Id)
{
    public string Name { get; private set; } = name;

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidDataException();
        if (name.Length > 300)
            throw new InvalidDataException();

        Name = name;

        // event?
    }
}
