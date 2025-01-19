namespace Medicine.Core.Aggregates;

public class Entity(Guid Id) : IEquatable<Entity>
{
    public Guid Id { get; private set; }

    public bool Equals(Entity? other)
    {
        if (other == null)
            return false;
        if (other.GetType() != GetType())
            return false;

        throw new NotImplementedException();
    }

    public static bool operator ==(Entity? left, Entity? right)
    {
        return left is not null && right is not null && left.Equals(right);
    }

    public static bool operator !=(Entity? left, Entity? right)
    {
        return !(left == right);
    }
}
