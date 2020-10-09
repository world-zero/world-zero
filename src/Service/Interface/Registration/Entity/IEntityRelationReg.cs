using System;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity;

namespace WorldZero.Service.Interface.Registration.Entity
{
    /// <inheritdoc cref="IEntityReg"/>
    /// <summary>
    /// This is a generic interface for entity relation creation service
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
    public abstract class IEntityRelationReg
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
    : IEntityReg<TEntityRelation, Id, int>
        where TEntityRelation : IEntityRelation
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TLeftEntity : IEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRightEntity : IEntity<TRightId, TRightBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
        where TRelationDTO : RelationDTO
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
    {
        protected new readonly IEntityRelationRepo
        <
            TEntityRelation,
            TLeftId,
            TLeftBuiltIn,
            TRightId,
            TRightBuiltIn,
            TRelationDTO
        >
        _repo;

        protected readonly IEntityRepo
        <
            TLeftEntity,
            TLeftId,
            TLeftBuiltIn
        >
        _leftRepo;

        protected readonly IEntityRepo
        <
            TRightEntity,
            TRightId,
            TRightBuiltIn
        >
        _rightRepo;

        protected IEntityRelationReg(
            IEntityRelationRepo
            <
                TEntityRelation,
                TLeftId,
                TLeftBuiltIn,
                TRightId,
                TRightBuiltIn,
                TRelationDTO
            >
            repo,
            IEntityRepo
            <
                TLeftEntity,
                TLeftId,
                TLeftBuiltIn
            >
            leftRepo,
            IEntityRepo
            <
                TRightEntity,
                TRightId,
                TRightBuiltIn
            >
            rightRepo
        )
            : base(repo)
        {
            this.AssertNotNull(repo, "repo");
            this.AssertNotNull(leftRepo, "leftRepo");
            this.AssertNotNull(rightRepo, "rightRepo");
            this._repo = repo;
            this._leftRepo = leftRepo;
            this._rightRepo = rightRepo;
        }

        /// <summary>
        /// This will store the supplied entity and save the repo. This will
        /// verify that the left and right IDs exist in their corresponding
        /// repos.
        /// </summary>
        public override TEntityRelation Register(TEntityRelation e)
        {
            this.PreRegisterChecks(e, "e");
            return base.Register(e);
        }

        /// <summary>
        /// Ensure that the entity is not null *and* that the IDs exist in the
        /// needed repos.
        /// </summary>
        protected override void PreRegisterChecks(TEntityRelation e, string t)
        {
            base.PreRegisterChecks(e, t);

            try
            { this._leftRepo.GetById(e.LeftId); }
            catch (ArgumentException)
            { throw new ArgumentException("Could not insert the relation entity as its left ID is not registered with the correct repo."); }

            try
            { this._rightRepo.GetById(e.RightId); }
            catch (ArgumentException)
            { throw new ArgumentException("Could not insert the relation entity as its right ID is not registered with the correct repo."); }
        }
    }
}