namespace TalentHub.ApplicationCore.Core.Abstractions;

public interface IEntity 
{
    Guid Id { get; }
}

public abstract class Entity :
    IEntity,
    IEquatable<Entity>
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public bool Equals(Entity? other) => other is not null && other.Id == Id;
    public override bool Equals(object? obj) => obj is Entity other && Equals(other);
    public override int GetHashCode() => Id.GetHashCode() * 37;

    public static bool operator ==(Entity? left, Entity? rigth) => left is not null && left.Equals(rigth);

    public static bool operator !=(Entity? left, Entity? rigth) => !(left == rigth);
}
