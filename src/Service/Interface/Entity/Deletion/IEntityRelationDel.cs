using System;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface;
using WorldZero.Data.Interface.Repository.Entity.Generic;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity;
using System.Threading.Tasks;

namespace WorldZero.Service.Interface.Entity.Deletion
{
    /// <inheritdoc cref="IEntityDel"/>
    /// <summary>
    /// This is a generic interface for entity relation deletion service
    /// classes.
    /// </summary>
    /// <typeparam name="TEntityRelation">
    /// The IEntityRelation implementation that this class abstracts.
    /// </typeparam>
    /// <typeparam name="TLeftEntity">
    /// This is the entity that maps to `TEntityRelation`'s `LeftId`.
    /// </typeparam>
    /// <typeparam name="TLeftId">
    /// This is the `ISingleValueObject` implementation that `TEntityRelation`
    /// uses as a left ID.
    /// </typeparam>
    /// <typeparam name="TLeftBuiltIn">
    /// This is the built-in type behind `TLeftId`.
    /// </typeparam>
    /// <typeparam name="TRightEntity">
    /// This is the entity that maps to `TEntityRelation`'s `RightId`.
    /// </typeparam>
    /// <typeparam name="TRightId">
    /// This is the `ISingleValueObject` implementation that `TEntityRelation`
    /// uses as a right ID.
    /// </typeparam>
    /// <typeparam name="TRightBuiltIn">
    /// This is the built-in type behind `TRightId`.
    /// </typeparam>
    /// <typeparam name="TRelationDTO">
    /// This is needed for the `TEntityRelationRepo`, but this class does not
    /// actually care about this type outside of that.
    /// </typeparam>
    /// <remarks>
    /// For ease of use, it is recommended to have a property similar to the
    /// below to easily and consistently cast up in children. This example is
    /// taken from CreateEra.
    /// <code>
    /// protected IEraRepo _eraRepo { get { return (IEraRepo) this._repo; } }
    /// </code>
    /// It is also worth noting that we don't actually care about the DTO type
    /// for the registration class itself - this is why there is no
    /// `IEntityRelationCntReg`.
    /// </remarks>
    public abstract class IEntityRelationDel
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
        : IEntityDel<TEntityRelation, Id, int>
        where TEntityRelation : IEntityRelation
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

        public IEntityRelationDel(
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

        public virtual void DeleteByLeftId(TLeftId id)
        {
            // Not tested because look at this thing.
            this.AssertNotNull(id, "id");
            this.BeginTransaction();
            this._relRepo.DeleteByLeftId(id);
            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new ArgumentException("Could not finish deleting by leftId.", e); }
        }

        public virtual async Task DeleteByLeftIdAsync(TLeftId id)
        {
            this.AssertNotNull(id, "id");
            await Task.Run(() => this.DeleteByLeftId(id));
        }

        public virtual void DeleteByRightId(TRightId id)
        {
            // Not tested because look at this thing.
            this.AssertNotNull(id, "id");
            this.BeginTransaction();
            this._relRepo.DeleteByRightId(id);
            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new ArgumentException("Could not finish deleting by rightId.", e); }
        }

        public virtual async Task DeleteByRightIdAsync(TRightId id)
        {
            this.AssertNotNull(id, "id");
            await Task.Run(() => this.DeleteByRightId(id));
        }

        public virtual void DeleteByDTO(TRelationDTO dto)
        {
            // Not tested because look at this thing.
            this.AssertNotNull(dto, "dto");
            this.BeginTransaction();
            this._relRepo.DeleteByDTO(dto);
            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new ArgumentException("Could not finish deleting by DTO.", e); }
        }

        public virtual async Task DeleteByDTOAsync(TRelationDTO dto)
        {
            this.AssertNotNull(dto, "dto");
            await Task.Run(() => this.DeleteByDTO(dto));
        }
    }
}