using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="IIdStatusedEntity"/>
    /// <summary>
    /// Task is a entity for a tuple of the Task table.
    /// </summary>
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

        /// <summary>
        /// This property controls whether or not the Historian ability can be
        /// used on this task.
        /// </summary>
        bool IsHistorianable { get; }
    }
}