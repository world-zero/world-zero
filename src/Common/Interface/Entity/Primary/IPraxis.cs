using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.Entity.Marker;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <remarks>
    /// MetaTaskId can be null.
    /// </remarks>
    /// <inheritdoc cref="IPraxisDTO"/>
    public interface IPraxis :
        IPraxisDTO,
        IIdStatusedEntity,
        IEntityHasOptional
    { }
}