using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.Entity.Marker;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="IMetaTaskDTO"/>
    public interface IMetaTask :
        IMetaTaskDTO,
        IIdStatusedEntity,
        IOptionalEntity
    { }
}