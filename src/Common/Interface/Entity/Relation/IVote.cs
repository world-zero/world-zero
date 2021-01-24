using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Interface.Entity.Relation
{
    /// <inheritdoc cref="IVoteDTO"/>
    /// <summary>
    /// Points must be (inclusive) between MinPoints and MaxPoints.
    /// <br/>
    /// Changing MinPoints or MaxPoints here will not update or re-validate
    /// existing votes.
    /// </summary>
    public interface IVote : IVoteDTO, IEntityRelation<Id, int, Id, int>
    { }
}