using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="ICommentDTO"/>
    public interface IComment : IEntityCntRelation<Id, int, Id, int>
    {
        Id PraxisId { get; }
        Id CharacterId { get; } 
        string Value { get; }
    }
}