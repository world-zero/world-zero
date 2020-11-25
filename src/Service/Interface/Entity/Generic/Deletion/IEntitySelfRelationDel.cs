using System;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Interface;
using WorldZero.Data.Interface.Repository.Entity.Generic;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity;
using System.Threading.Tasks;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
{
    /// <inheritdoc cref="IEntityDel"/>
    /// <summary>
    /// This abstract subclass is used for relations between the same entity.
    /// This results in `DeleteByLeftId()` and `DeleteByRightId()` performing
    /// the same process to ensure that the order of the relation is ignored.
    ///</summary>
    public abstract class IEntitySelfRelationDel
    <
        TEntityRelation,
        TEntity,
        TId,
        TBuiltIn,
        TRelationDTO
    >
        : IEntityRelationDel
        <
            TEntityRelation,
            TEntity,
            TId,
            TBuiltIn,
            TEntity,
            TId,
            TBuiltIn,
            TRelationDTO
        >
        where TEntityRelation : IEntitySelfRelation
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

        public IEntitySelfRelationDel(
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