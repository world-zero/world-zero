using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="ITaskDTO"/>
    public interface ITask : IIdStatusedEntity
    {
        string Summary { get; }
        Name FactionId { get; }
        PointTotal Points { get; }

        /// <remarks>
        /// Be cautious of <see cref="IEra.TaskLevelBuffer"/>.
        /// </remarks>
        Level Level { get; }
        Level MinLevel { get; }
        bool IsHistorianable { get; }
    }
}