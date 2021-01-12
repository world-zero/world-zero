using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.DTO.Entity.Primary
{
    /// <summary>
    /// Task is a entity for a tuple of the Task table.
    /// </summary>
    public interface ITaskDTO : IIdStatusedDTO
    {
        string Summary { get; }
        Name FactionId { get; }
        PointTotal Points { get; }
        Level Level { get; }
        Level MinLevel { get; }

        /// <summary>
        /// This property controls whether or not the Historian ability can be
        /// used on this task.
        /// </summary>
        bool IsHistorianable { get; }
    }
}