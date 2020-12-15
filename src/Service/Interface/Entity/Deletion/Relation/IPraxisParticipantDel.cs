using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Interface.Entity.Deletion.Primary;

namespace WorldZero.Service.Interface.Entity.Deletion.Relation
{
    /// <remarks>
    /// A Praxis should always have at least one participant. As a result,
    /// these methods will throw an exception if they are going to remove a
    /// praxis' final participant. However, the methods beginning with `Sudo`
    /// that take a `PraxisDel` class will delete the praxis if the last
    /// participant is removed.
    /// <br />
    /// If a participant of a duel is deleted, then the praxis will be updated
    /// to no longer be a duel. This does not use the praxis updating service
    /// class.
    /// <br />
    /// This will also delete the participant's received votes. Whether or not
    /// the voting character will receive a refund is up to <see
    /// cref="VoteDel"/>.
    /// </remarks>
    /// <inheritdoc cref="IEntityRelationDel{TEntityRelationCnt, TLeftEntity, TLeftId, TLeftBuiltIn, TRightEntity, TRightId, TRightBuiltIn, TRelationDTO}"/>
    public interface IPraxisParticipantDel
        : IEntityRelationDel
        <
            IPraxisParticipant,
            IPraxis, Id, int,
            ICharacter, Id, int,
            RelationDTO<Id, int, Id, int>
        >
    {
        void DeleteByPraxis(IPraxis p);
        void DeleteByPraxis(Id praxisId);
        Task DeleteByPraxisAsync(IPraxis p);
        Task DeleteByPraxisAsync(Id praxisId);

        void DeleteByCharacter(ICharacter c);
        void DeleteByCharacter(Id charId);
        Task DeleteByCharacterAsync(ICharacter c);
        Task DeleteByCharacterAsync(Id charId);

        void SudoDeleteByCharacter(ICharacter c, IPraxisDel praxisDel);
        void SudoDeleteByCharacter(Id charId, IPraxisDel praxisDel);
        Task SudoDeleteByCharacterAsync(ICharacter c, IPraxisDel praxisDel);
        Task SudoDeleteByCharacterAsync(Id charId, IPraxisDel praxisDel);
    }
}