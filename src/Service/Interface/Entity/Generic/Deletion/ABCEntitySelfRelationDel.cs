using System;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Data.Interface.Repository.Entity.Generic;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using System.Threading.Tasks;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntitySelfRelationDel{TEntityRelation, TEntity, TId, TBuiltIn, TRelationDTO}"/>
    public abstract class ABCEntitySelfRelationDel
    <
        TEntityRelation,
        TEntity,
        TId,
        TBuiltIn,
        TRelationDTO
    >
        : ABCEntityRelationDel
        <
            TEntityRelation,
            TEntity,
            TId,
            TBuiltIn,
            TEntity,
            TId,
            TBuiltIn,
            TRelationDTO
        >,
        IEntitySelfRelationDel
        <
            TEntityRelation,
            TEntity,
            TId,
            TBuiltIn,
            TRelationDTO
        >
        where TEntityRelation : class, IEntitySelfRelation
            <TId, TBuiltIn>
        where TEntity : IEntity<TId, TBuiltIn>
        where TId  : ISingleValueObject<TBuiltIn>
        where TRelationDTO : RelationDTO
            <TId, TBuiltIn, TId, TBuiltIn>
    {
        protected
        IEntitySelfRelationRepo<TEntityRelation, TId, TBuiltIn, TRelationDTO>
        _selfRelRepo
        {
            get
            {
                return
                (IEntitySelfRelationRepo<TEntityRelation, TId, TBuiltIn, TRelationDTO>)
                this._relRepo;
            }
        }

        public ABCEntitySelfRelationDel(
            IEntitySelfRelationRepo
            <
                TEntityRelation,
                TId,
                TBuiltIn,
                TRelationDTO
            >
            repo
        )
            : base(repo)
        { }

        public void DeleteByRelatedId(TId id)
        {
            this.AssertNotNull(id, "id");
            this.BeginTransaction();
            this._selfRelRepo.DeleteByRelatedId(id);
            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new ArgumentException("Could not complete the deletion via relateed ID.", e); }
        }

        public async Task DeleteByRelatedIdAsync(TId id)
        {
            await Task.Run(() => this.DeleteByRelatedId(id));
        }
    }
}