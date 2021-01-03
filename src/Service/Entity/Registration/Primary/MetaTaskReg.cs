using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Service.Interface.Entity.Registration.Primary;
using WorldZero.Service.Constant.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IMetaTaskReg"/>
    public class MetaTaskReg
        : ABCEntityReg<IMetaTask, Id, int>, IMetaTaskReg
    {
        protected IMetaTaskRepo _metaTaskRepo
        { get { return (IMetaTaskRepo) this._repo; } }

        protected readonly IFactionRepo _factionRepo;
        protected readonly IStatusRepo _statusRepo;

        public MetaTaskReg(
            IMetaTaskRepo metaTaskRepo,
            IFactionRepo factionRepo,
            IStatusRepo statusRepo
        )
            : base(metaTaskRepo)
        {
            if (factionRepo == null) throw new ArgumentNullException("factionRepo");
            if (statusRepo == null) throw new ArgumentNullException("statusRepo");
            this._factionRepo = factionRepo;
            this._statusRepo = statusRepo;
        }

        /// <summary>
        /// Create the meta task and save them. This will ensure that the
        /// meta task has a valid faction ID and status ID.
        /// </summary>
        public override IMetaTask Register(IMetaTask mt)
        {
            this.AssertNotNull(mt, "mt");
            if (   (mt.StatusId != ConstantStatuses.InProgress.Id)
                && (mt.StatusId != ConstantStatuses.Active.Id)   )
            {
                throw new ArgumentException("A meta task can only be Active or In Progress");
            }

            this._metaTaskRepo.BeginTransaction(true);
            try
            {
                this._factionRepo.GetById(mt.FactionId);
            }
            catch (ArgumentException)
            {
                this._metaTaskRepo.DiscardTransaction();
                throw new ArgumentException($"MetaTask of ID {mt.Id.Get} has an invalid Factionn ID of {mt.FactionId.Get}.");
            }
            try
            {
                var r = base.Register(mt);
                this._metaTaskRepo.EndTransaction();
                return r;
            }
            catch (ArgumentException exc)
            {
                this._metaTaskRepo.DiscardTransaction();
                throw new ArgumentException("Could not complete the registration.", exc);
            }
        }
    }
}