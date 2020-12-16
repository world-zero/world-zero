using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Registration;

namespace WorldZero.Service.Interface.Entity.Registration.Relation
{
    /// <remarks>
    /// This will immediately award the participant with the amount of
    /// `Vote.Points` to their `Character.VotePointsLeft` field. This does not
    /// use the Character updating service class.
    /// <br />
    /// Naturally, Players cannot vote for themselves, and Players cannot vote
    /// several times on a praxis. Note that this describes a Player.
    /// </remarks>
    /// <inheritdoc cref="IEntityRelationReg{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRightEntity, TRightId, TRightBuiltIn, TRelationDTO}"/>
    public interface IVoteReg
        : IEntityRelationReg
        <
            IVote,
            ICharacter,
            Id,
            int,
            IPraxisParticipant,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
        >
    { }
}