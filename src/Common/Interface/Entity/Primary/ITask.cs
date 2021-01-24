using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <remarks>
    /// With Level, be cautious of <see cref="IEra.TaskLevelBuffer"/>.
    /// </remarks>
    /// <inheritdoc cref="ITaskDTO"/>
    public interface ITask : ITaskDTO, IIdStatusedEntity
    { }
}