using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.Interface.Entity.Marker;

namespace WorldZero.Common.Interface.Entity.Primary
{
    /// <inheritdoc cref="IMetaTaskDTO"/>
    public interface IMetaTask : IIdStatusedEntity, IOptionalEntity
    {
        string Description { get; }
        bool IsFlatBonus { get; }
        PointTotal Bonus { get; }
        Name FactionId { get; }
    }
}