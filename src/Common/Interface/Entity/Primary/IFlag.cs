using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="INamedEntity"/>
    /// <summary>
    /// Flag is a entity for a tuple of the Flag table.
    /// </summary>
    public interface IFlag : INamedEntity
    {
        /// <remarks>
        /// This is can be `null`.
        /// </remarks>
        string Description { get; }

        /// <summary>
        /// IsFlatPenalty determines whether `Penalty` is a flat penalty point
        /// deduction or if it is a point percentage deduction.
        /// </summary>
        bool IsFlatPenalty { get; }

        /// <summary>
        /// Penalty is a PointTotal that can be either a flag point
        /// penalty or a point percentage modifier. For more, see
        /// <see cref="IFlag.IsFlatPenalty"/>.
        /// </summary>
        /// <remarks>
        /// If `IsFlatPenalty` is false, then this will act as the percentage
        /// of the point total to deduct.
        /// </remarks>
        PointTotal Penalty { get; }
    }
}