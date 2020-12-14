using System;
using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Service.Interface.Entity.Registration.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="IMetaTaskFlagReg"/>
    public class MetaTaskFlagReg
        : ABCEntityRelationReg
        <
            IMetaTaskFlag,
            IMetaTask,
            Id,
            int,
            IFlag,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >, IMetaTaskFlagReg
    {
        protected IMetaTaskFlagRepo _metaTaskFlagRepo
        { get { return (IMetaTaskFlagRepo) this._repo; } }

        protected IMetaTaskRepo _metaTaskRepo
        { get { return (IMetaTaskRepo) this._leftRepo; } }

        protected IFlagRepo _flagRepo
        { get { return (IFlagRepo) this._rightRepo; } }

        public MetaTaskFlagReg(
            IMetaTaskFlagRepo metaTaskFlagRepo,
            IMetaTaskRepo metaTaskRepo,
            IFlagRepo flagRepo
        )
            : base(metaTaskFlagRepo, metaTaskRepo, flagRepo)
        { }

        public override IMetaTaskFlag Register(IMetaTaskFlag e)
        {
            // NOTE: This code exists in TaskFlagReg.Register(),
            // MetaTaskFlagReg.Register(), and PraxisFlagReg.Register()
            // because of the weird non-generic abstract classes that exist
            // right before the entities are implemented.
            this.AssertNotNull(e, "e");
            IMetaTask mt;
            IFlag f;
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

            ((UnsafeMetaTask) mt).Bonus = PointTotal
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