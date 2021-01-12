using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="IFlagDTO"/>
    public interface IFlag : INamedEntity
    {
        /// <remarks>
        /// This is can be `null`.
        /// </remarks>
        string Description { get; }

        bool IsFlatPenalty { get; }

        PointTotal Penalty { get; }
    }
}