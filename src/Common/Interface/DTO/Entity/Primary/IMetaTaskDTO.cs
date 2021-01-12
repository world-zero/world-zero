using WorldZero.Common.Interface.DTO.Entity.Unspecified.Primary;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.DTO.Entity.Primary
{
    /// <summary>
    /// MetaTask is a entity for a tuple of the MetaTask table.
    /// </summary>
    public interface IMetaTaskDTO : IIdStatusedDTO
    {
        string Description { get; }

        /// <summary>
        /// IsFlatBonus determines whether `Bonus` is a flat bonus point
        /// addition or if it is a point percentage modifier.
        /// </summary>
        bool IsFlatBonus { get; }

        /// <summary>
        /// Bonus is a PointTotal that can be either a flag point
        /// bonus or a point percentage modifier. For more, see
        /// <see cref="IMetaTaskDTO.IsFlatBonus"/>.
        /// </summary>
        /// <remarks>
        /// If `IsFlatBonus` is false, then this will act as the percentage
        /// of the point total to add.
        /// </remarks>
        PointTotal Bonus { get; }

        /// <summary>
        /// This is the ID of the faction that sponsors this meta task.
        /// </summary>
        Name FactionId { get; }
    }
}