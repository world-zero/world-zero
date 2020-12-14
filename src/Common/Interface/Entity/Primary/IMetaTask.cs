using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Marker;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="IIdStatusedEntity"/>
    /// <summary>
    /// MetaTask is a entity for a tuple of the MetaTask table.
    /// </summary>
    public interface IMetaTask : IIdStatusedEntity, IOptionalEntity
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
        /// <see cref="IMetaTask.IsFlatBonus"/>.
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