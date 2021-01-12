using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IFoeDTO"/>
    public interface IFoe : IEntitySelfRelation<Id, int>
    {
        Id FirstCharacterId { get; }
        Id SecondCharacterId { get; }
    }
}