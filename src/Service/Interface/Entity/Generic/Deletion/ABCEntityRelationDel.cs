using System.Threading.Tasks;
using WorldZero.Common.ValueObject.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Data.Interface.Repository.Entity.Generic;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntityRelationDel{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRightEntity, TRightId, TRightBuiltIn, TRelationDTO}"/>
    public abstract class ABCEntityRelationDel
    <
        TEntityRelation,
        TLeftEntity,
        TLeftId,
        TLeftBuiltIn,
        TRightEntity,
        TRightId,
        TRightBuiltIn,
        TRelationDTO
    >
        : ABCEntityDel<TEntityRelation, Id, int>,
          IEntityRelationDel
            <
                TEntityRelation,
                TLeftEntity,
                TLeftId,
                TLeftBuiltIn,
                TRightEntity,
                TRightId,
                TRightBuiltIn,
                TRelationDTO
            >
        where TEntityRelation : class, IEntityRelation
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftEntity : IEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRightEntity : IEntity<TRightId, TRightBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
    {
        protected IEntityRelationRepo
        <
            TEntityRelation,
            TLeftId,
            TLeftBuiltIn,
            TRightId,
            TRightBuiltIn,
            TRelationDTO
        >
        _relRepo
        {
            get
            {
                return (IEntityRelationRepo
                    <
                        TEntityRelation,
                        TLeftId,
                        TLeftBuiltIn,
                        TRightId,
                        TRightBuiltIn,
                        TRelationDTO
                    >) this._repo;
            }
        }

        public ABCEntityRelationDel(
            IEntityRelationRepo
            <
                TEntityRelation,
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

        public virtual void DeleteByLeft(TLeftId id)
        {
            this.Transaction<TLeftId>(this._relRepo.DeleteByLeftId, id);
        }

        public virtual void DeleteByLeft(TLeftEntity e)
        {
            this.AssertNotNull(e, "e");
            this.DeleteByLeft(e.Id);
        }

        public virtual async Task DeleteByLeftAsync(TLeftEntity e)
        {
            this.AssertNotNull(e, "e");
            await Task.Run(() => this.DeleteByLeftAsync(e.Id));
        }

        public virtual async Task DeleteByLeftAsync(TLeftId id)
        {
            this.AssertNotNull(id, "id");
            await Task.Run(() => this.DeleteByLeft(id));
        }

        public virtual void DeleteByRight(TRightId id)
        {
            this.Transaction<TRightId>(this._relRepo.DeleteByRightId, id);
        }

        public virtual void DeleteByRight(TRightEntity e)
        {
            this.AssertNotNull(e, "e");
            this.DeleteByRight(e.Id);
        }

        public virtual async Task DeleteByRightAsync(TRightEntity e)
        {
            this.AssertNotNull(e, "e");
            await Task.Run(() => this.DeleteByRightAsync(e.Id));
        }

        public virtual async Task DeleteByRightAsync(TRightId id)
        {
            this.AssertNotNull(id, "id");
            await Task.Run(() => this.DeleteByRight(id));
        }

        public virtual void DeleteByDTO(TRelationDTO dto)
        {
            this.Transaction<TRelationDTO>(this._relRepo.DeleteByDTO, dto);
        }

        public virtual async Task DeleteByDTOAsync(TRelationDTO dto)
        {
            this.AssertNotNull(dto, "dto");
            await Task.Run(() => this.DeleteByDTO(dto));
        }
    }
}