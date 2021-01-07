using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Registration;

namespace WorldZero.Service.Interface.Entity.Registration.Relation
{
    /// <remarks>
    /// A Praxis should always have at least one participant.
    /// <br />
    /// This will allow characters to participant on a task, regardless of the
    /// task's faction or their faction.
    /// <br />
    /// This class will ensure that a duel is between two characters.
    /// <br />
    /// This will ensure that one Player is not having several of their
    /// characters participating on the same praxis.
    /// <br />
    /// The character's level versus the task's level is computed here, as they
    /// register with / on a praxis. This will allow someone to register as In
    /// Progress for a praxis and still be able to complete it after an Era
    /// roll-over. For example, if someone's EraLevel is X, and
    /// `Era.TaskLevelBuffer` is Y, then someone can be a participant of tasks
    /// of level X+Y and below.
    /// <br />
    /// This will ensure that a character does not have more than the allowed
    /// number of in progress / active praxises, as defined by the Era returned
    /// by `EraReg.GetActiveEra()`. That said, if someone has X in progress
    /// praxises and the new Era has the MaxPraxises of X-3, then they will
    /// keep their in progress praxises despite being over the limit. This is
    /// an example of the precedent described in
    /// `doc/backend/EntityServiceClasses.md`.
    /// <br />
    /// When furthering development, be mindful about how PraxisReg needs a
    /// participant - both PraxisReg and PraxisParticipantReg rely on this
    /// fact.
    /// </remarks>
    /// <inheritdoc cref="IEntityRelationReg{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRightEntity, TRightId, TRightBuiltIn, TRelationDTO}"/>
    public interface IPraxisParticipantReg
        : IEntityRelationReg
        <
            IPraxisParticipant,
            IPraxis,
            Id,
            int,
            ICharacter,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
        >
    { }
}