using System;
using WorldZero.Service.Interface.Entity.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg"/>
    public class MetaTaskReg
        : IEntityReg<UnsafeMetaTask, Id, int>
    {
        protected IUnsafeMetaTaskRepo _metaTaskRepo
        { get { return (IUnsafeMetaTaskRepo) this._repo; } }

        protected readonly IUnsafeFactionRepo _factionRepo;
        protected readonly IUnsafeStatusRepo _statusRepo;

        public MetaTaskReg(
            IUnsafeMetaTaskRepo metaTaskRepo,
            IUnsafeFactionRepo factionRepo,
            IUnsafeStatusRepo statusRepo
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
        public override UnsafeMetaTask Register(UnsafeMetaTask mt)
        {
            this.AssertNotNull(mt, "mt");
            if (   (mt.StatusId != StatusReg.InProgress.Id)
                && (mt.StatusId != StatusReg.Active.Id)   )
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