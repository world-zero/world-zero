using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using System.Threading.Tasks;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntityRelationDel"/>
    public abstract class IEntityRelationCntDel
    <
        TEntityRelationCnt,
        TLeftEntity,
        TLeftId,
        TLeftBuiltIn,
        TRightEntity,
        TRightId,
        TRightBuiltIn,
        TRelationDTO
    >
        : IEntityRelationDel
        <
            TEntityRelationCnt,
            TLeftEntity,
            TLeftId,
            TLeftBuiltIn,
            TRightEntity,
            TRightId,
            TRightBuiltIn,
            TRelationDTO
        >
        where TEntityRelationCnt : IUnsafeEntityCntRelation
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftEntity : IUnsafeEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRightEntity : IUnsafeEntity<TRightId, TRightBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
        where TRelationDTO : CntRelationDTO
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
    {
        protected IEntityRelationCntRepo
        <
            TEntityRelationCnt,
            TLeftId,
            TLeftBuiltIn,
            TRightId,
            TRightBuiltIn,
            TRelationDTO
        >
        _cntRepo
        {
            get
            {
                return (IEntityRelationCntRepo
                <
                    TEntityRelationCnt,
                    TLeftId,
                    TLeftBuiltIn,
                    TRightId,
                    TRightBuiltIn,
                    TRelationDTO
                >) this._relRepo;
            }
        }

        public IEntityRelationCntDel(
            IEntityRelationCntRepo
            <
                TEntityRelationCnt,
                TLeftId,
                TLeftBuiltIn,
                TRightId,
                TRightBuiltIn,
                TRelationDTO
            >
            repo
        )
            : base(repo)
        { }

        public virtual void DeleteByPartialDTO(
            RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto
        )
        {
            this.Transaction
            <RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>>(
                this._cntRepo.DeleteByPartialDTO,
                dto
            );
        }

        public virtual async Task DeleteByPartialDTOAsync(
            RelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto
        )
        {
            this.AssertNotNull(dto, "dto");
            await Task.Run(() => this.DeleteByPartialDTO(dto));
        }
    }
}