using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    /// <inheritdoc cref="IEntity"/>
    /// <summary>
    /// This class is used for entities that have a `Id` primary key.
    /// </summary>
    /// <remarks>
    /// This value will be 0 when unspecified as the unset state and if not
    /// supplied to the appropriate constructor. This is done to allow for
    /// instantiation and insertion to a repo at a later time as repos are
    /// responsible for auto-generated IDs.
    /// </remarks>
    public interface IIdEntity : IEntity<Id, int>
    { }
}