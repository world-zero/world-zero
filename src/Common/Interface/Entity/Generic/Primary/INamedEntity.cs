using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    /// <inheritdoc cref="WorldZero.Common.Interface.Entity.Generic.Primary.IEntity{TId, TBuiltIn}"/>
    /// <summary>
    /// This class is used for entities that have a `Name` primary key. This
    /// can be either null for unset, or set.
    /// </summary>
    public interface INamedEntity : IEntity<Name, string>
    { }
}