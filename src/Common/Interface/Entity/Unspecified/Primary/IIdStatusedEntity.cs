using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.Entity.Unspecified.Primary
{
    /// <inheritdoc cref="IIdEntity"/>
    /// <summary>
    /// This derivation contains a `StatusId`, which is a name denoting the
    /// concrete entity's status.
    /// </summary>
    public interface IIdStatusedEntity : IIdStatusedDTO, IIdEntity
    { }
}