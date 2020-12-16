using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    /// <inheritdoc cref="IIdEntity"/>
    /// <summary>
    /// This derivation contains a `StatusId`, which is a name denoting the
    /// concrete entity's status.
    /// </summary>
    public interface IIdStatusedEntity : IIdEntity
    {
        Name StatusId { get; }
    }
}