using System;
using WorldZero.Service.Interface.Entity.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="IEntityRelationReg"/>
    public class MetaTaskFlagReg
        : IEntityRelationReg
        <
            MetaTaskFlag,
            UnsafeMetaTask,
            Id,
            int,
            UnsafeFlag,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >
    {
        protected IMetaTaskFlagRepo _metaTaskFlagRepo
        { get { return (IMetaTaskFlagRepo) this._repo; } }

        protected IUnsafeMetaTaskRepo _metaTaskRepo
        { get { return (IUnsafeMetaTaskRepo) this._leftRepo; } }

        protected IUnsafeFlagRepo _flagRepo
        { get { return (IUnsafeFlagRepo) this._rightRepo; } }

        public MetaTaskFlagReg(
            IMetaTaskFlagRepo metaTaskFlagRepo,
            IUnsafeMetaTaskRepo metaTaskRepo,
            IUnsafeFlagRepo flagRepo
        )
            : base(metaTaskFlagRepo, metaTaskRepo, flagRepo)
        { }

        public override MetaTaskFlag Register(MetaTaskFlag e)
        {
            // NOTE: This code exists in TaskFlagReg.Register(),
            // MetaTaskFlagReg.Register(), and PraxisFlagReg.Register()
            // because of the weird non-generic abstract classes that exist
            // right before the entities are implemented.
            this.AssertNotNull(e, "e");
            UnsafeMetaTask mt;
            UnsafeFlag f;
            this._metaTaskFlagRepo.BeginTransaction(true);
            try
            {
                mt = this.GetLeftEntity(e);
                f = this.GetRightEntity(e);
            } catch (ArgumentException exc)
            {
                this._metaTaskFlagRepo.DiscardTransaction();
                throw new ArgumentException("Could not retrieve an associated entity.", exc);
            }

            mt.Bonus = PointTotal
                .ApplyPenalty(mt.Bonus, f.Penalty, f.IsFlatPenalty);

            try
            {
                this._metaTaskRepo.Update(mt);
                this._metaTaskFlagRepo.Insert(e);
                this._metaTaskFlagRepo.EndTransaction();
                return e;
            }
            catch (ArgumentException exc)
            {
                this._metaTaskFlagRepo.DiscardTransaction();
                throw new ArgumentException("Could not complete the registration.", exc);
            }
        }
    }
}