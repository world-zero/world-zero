using System;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using System.Threading.Tasks;

namespace WorldZero.Service.Interface.Entity.Generic.Deletion
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
        where TEntityRelation : ABCEntityRelation
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftEntity : ABCEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRightEntity : ABCEntity<TRightId, TRightBuiltIn>
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