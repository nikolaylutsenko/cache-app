namespace Medicine.Domain.Aggregates;

public abstract class EntityBase<TIdentity>(TIdentity id) : IEquatable<EntityBase<TIdentity>>
    where TIdentity : IdentityBase
{
    public TIdentity Id { get; private set; } = id;

    public bool Equals(EntityBase<TIdentity>? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return Equals(Id, other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        return Equals((EntityBase<TIdentity>)obj);
    }

    public override int GetHashCode() => GetType().GetHashCode() * 907 + Id.GetHashCode();

    public override string ToString() => $"{GetType().Name}#[Identity={Id}]";

    public static bool operator ==(EntityBase<TIdentity> a, EntityBase<TIdentity> b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(EntityBase<TIdentity> a, EntityBase<TIdentity> b)
    {
        return !(a == b);
    }
}
