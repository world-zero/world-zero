using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    /// <inheritdoc cref="IIdEntity"/>
    /// <remarks>
    /// This class exists to have entities that have an `Id` ID property, and
    /// also have a required name property - critically, this name must be
    /// unique. As with this type of rule, uniqueness should be enforced by the
    /// repo. This name can only be valid.
    /// </remarks>
    public interface IIdNamedEntity : IEntity<Id, int>
    {
        /// <summary>
        /// This is a `Name` that is not an ID of an entity, but rather is a
        /// `Name` that a repo must enforce to be unique.
        /// </summary>
        Name Name { get; }
    }
}