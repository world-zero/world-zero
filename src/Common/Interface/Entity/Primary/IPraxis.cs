using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.Entity.Marker;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="IPraxisDTO"/>
    public interface IPraxis : IIdStatusedEntity, IEntityHasOptional
    {
        Id TaskId { get; }
        PointTotal Points { get; }

        /// <remarks>
        /// This can be `null`.
        /// </remarks>
        Id MetaTaskId { get; }

        bool AreDueling { get; }
    }
}