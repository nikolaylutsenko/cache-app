namespace Medicine.Domain.Aggregates;

public abstract class IdentityBase(Guid id) : ValueObjectBase
{
    public Guid Id { get; private set; } = id;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }

    public override string ToString() => $"{GetType().Name}:{Id}";

    public static implicit operator Guid(IdentityBase id) => id.Id;
}
