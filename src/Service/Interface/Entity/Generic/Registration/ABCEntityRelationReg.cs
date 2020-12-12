using System;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.ValueObject;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using System.Threading.Tasks;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Service.Interface.Entity.Generic.Registration
{
    /// <inheritdoc cref="IEntityRelationReg{TEntityRelation, TLeftEntity, TLeftId, TLeftBuiltIn, TRightEntity, TRightId, TRightBuiltIn, TRelationDTO"/>
    public abstract class ABCEntityRelationReg
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
        : ABCEntityReg<TEntityRelation, Id, int>,
          IEntityRelationReg
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
        where TLeftEntity : class, IEntity<TLeftId, TLeftBuiltIn>
        where TLeftId  : ISingleValueObject<TLeftBuiltIn>
        where TRightEntity : class, IEntity<TRightId, TRightBuiltIn>
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

        protected ABCEntityRelationReg(
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
            this.AssertNotNull(leftRepo, "leftRepo");
            this.AssertNotNull(rightRepo, "rightRepo");
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
            this.AssertNotNull(e, "e");
            this._relRepo.BeginTransaction(true);
            try
            {
                this.GetLeftEntity(e);
                this.GetRightEntity(e);
            }
            catch (ArgumentNullException exc)
            {
                this._relRepo.DiscardTransaction();
                throw new ArgumentNullException("See trace.", exc);
            }
            catch (ArgumentException exc)
            {
                this._relRepo.DiscardTransaction();
                throw new ArgumentException("An error occurred during the registration of a relational entity.", exc);
            }
            try
            {
                var r = base.Register(e);
                this._relRepo.EndTransaction();
                return r;
            }
            catch (ArgumentException exc)
            {
                this._relRepo.DiscardTransaction();
                throw new ArgumentException("Could not complete the registration", exc);
            }
        }

        public async override Task<TEntityRelation> RegisterAsync(TEntityRelation e)
        {
            return await Task.Run(() => this.Register(e));
        }

        /// <summary>
        /// This will get and return the left entity associated with the
        /// supplied relational entity, throwing an `ArgumentException` if an
        /// error occurs.
        /// </summary>
        /// <remarks>
        /// This *will not* start a serialized transaction.
        /// </remarks>
        protected TLeftEntity GetLeftEntity(TEntityRelation e)
        {
            try
            {
                return this._leftRepo.GetById(e.LeftId);
            }
            catch (ArgumentException exc)
            {
                throw new ArgumentException("Could not retrieve the LeftId.", exc);
            }
        }

        protected async Task<TLeftEntity> GetLeftEntityAsync(TEntityRelation e)
        {
            return await Task.Run(() => this.GetLeftEntity(e));
        }

        /// <summary>
        /// This will get and return the right entity associated with the
        /// supplied relational entity, throwing an `ArgumentException` if an
        /// error occurs.
        /// </summary>
        /// <remarks>
        /// This *will not* start a serialized transaction.
        /// </remarks>
        protected TRightEntity GetRightEntity(TEntityRelation e)
        {
            try
            {
                return this._rightRepo.GetById(e.RightId);
            }
            catch (ArgumentException exc)
            {
                throw new ArgumentException("Could not retrieve the RightId.", exc);
            }
        }

        protected async Task<TRightEntity> GetRightEntityAsync(TEntityRelation e)
        {
            return await Task.Run(() => this.GetRightEntity(e));
        }
    }
}