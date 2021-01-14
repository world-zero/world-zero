using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Data.Interface.Repository.Entity.Generic;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using System.Threading.Tasks;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntityRelationCntDel{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRightEntity, TRightId, TRightBuiltIn, TRelationDTO}"/>
    public abstract class ABCEntityRelationCntDel
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
        : ABCEntityRelationDel
        <
            TEntityRelationCnt,
            TLeftEntity,
            TLeftId,
            TLeftBuiltIn,
            TRightEntity,
            TRightId,
            TRightBuiltIn,
            TRelationDTO
        >,
        IEntityRelationCntDel
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
        where TEntityRelationCnt : class, IEntityCntRelation
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftEntity : IEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ABCSingleValueObject<TLeftBuiltIn>
        where TRightEntity : IEntity<TRightId, TRightBuiltIn>
        where TRightId : ABCSingleValueObject<TRightBuiltIn>
        where TRelationDTO : NoIdCntRelationDTO
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

        public ABCEntityRelationCntDel(
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
            NoIdRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto
        )
        {
            this.Transaction
            <NoIdRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>>(
                this._cntRepo.DeleteByPartialDTO,
                dto
            );
        }

        public virtual async Task DeleteByPartialDTOAsync(
            NoIdRelationDTO<TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn> dto
        )
        {
            this.AssertNotNull(dto, "dto");
            await Task.Run(() => this.DeleteByPartialDTO(dto));
        }
    }
}