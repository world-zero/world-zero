using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Interface.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IEntityRelationCntDel{TEntityRelationCnt, TLeftEntity, TLeftId, TLeftBuiltIn, TRightEntity, TRightId, TRightBuiltIn, TRelationDTO}"/>
    public interface ICommentDel
        : IEntityRelationCntDel
        <
            IComment,
            IPraxis, Id, int,
            ICharacter, Id, int,
            NoIdCntRelationDTO<Id, int, Id, int>
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
    }
}