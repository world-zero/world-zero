using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Interface.Entity.Deletion.Relation
{
    /// <remarks>
    /// This will not refund the vote points used.
    /// </remarks>
    /// <inheritdoc cref="IEntityRelationDel{TEntityRelationCnt, TLeftEntity, TLeftId, TLeftBuiltIn, TRightEntity, TRightId, TRightBuiltIn, TRelationDTO}"/>
    public interface IVoteDel
        : IEntityRelationDel
        <
            IVote,
            ICharacter, Id, int,
            IPraxisParticipant, Id, int,
            NoIdRelationDTO<Id, int, Id, int>
        >
    {
        void DeleteByCharacter(ICharacter c);
        void DeleteByCharacter(Id charId);
        Task DeleteByCharacterAsync(ICharacter c);
        Task DeleteByCharacterAsync(Id charId);

        void DeleteByPraxisParticipant(IPraxisParticipant pp);
        void DeleteByPraxisParticipant(Id ppId);
        Task DeleteByPraxisParticipantAsync(IPraxisParticipant pp);
        Task DeleteByPraxisParticipantAsync(Id ppId);
    }
}