using System;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity;
using System.Threading.Tasks;

namespace WorldZero.Service.Interface.Entity.Deletion
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
        where TEntityRelation : IEntityRelation
            <TId, TBuiltIn, TId, TBuiltIn>
        where TEntity : IEntity<TId, TBuiltIn>
        where TId  : ISingleValueObject<TBuiltIn>
        where TRelationDTO : RelationDTO
            <TId, TBuiltIn, TId, TBuiltIn>
    {
        public IEntitySelfRelationDel(
            IEntityRelationRepo
            <
                TEntityRelation,
                TId,
                TBuiltIn,
                TId,
                TBuiltIn,
                TRelationDTO
            >
            repo
        )
            : base(repo)
        { }

        public override void DeleteByLeftId(TId id)
        {
            this._deleteByLeftRight(id);
        }

        public override void DeleteByRightId(TId id)
        {
            this._deleteByLeftRight(id);
        }

        private void _deleteByLeftRight(TId id)
        {
            this.AssertNotNull(id, "id");
            this.BeginTransaction();
            base.DeleteByLeftId(id);
            base.DeleteByRightId(id);
            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new ArgumentException("Could not finish deleting by a left or right ID.", e); }
        }
    }
}