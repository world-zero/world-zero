using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary
{
    /// <summary>
    /// This extension has a name in addition to an <see cref="Id"/> ID.
    /// </summary>
    public interface IIdNamedDTO : IEntityDTO<Id, int>
    {
        /// <summary>
        /// This is a `Name` that is not an ID of an entity, but rather is a
        /// `Name` that a repo must enforce to be unique.
        /// </summary>
        Name Name { get; }
    }
}