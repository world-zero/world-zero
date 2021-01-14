using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Interface.Entity.Deletion.Relation
{
    /// <inheritdoc cref="IEntityRelateRelationalDel{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TLEntityRelation, TLLeftId, TLLeftBuiltIn, TLRightId, TLRightBuiltIn, TLRelationDTO, TRightEntity, TRightId, TRightBuiltIn, TRelationDTO}"/>
    public interface ICommentFlagDel
        : IEntityRelateRelationalDel
        <
            ICommentFlag,
            IComment, Id, int,
                IComment, Id, int, Id, int, NoIdCntRelationDTO<Id, int, Id, int>,
            IFlag, Name, string,
            NoIdRelationDTO<Id, int, Name, string>
        >
    {
        void DeleteByComment(IComment c);
        void DeleteByComment(Id commentId);
        Task DeleteByCommentAsync(IComment c);
        Task DeleteByCommentAsync(Id commentId);
    }
}