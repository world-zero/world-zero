using System;
using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Registration.Entity
{
    public class MetaTaskReg
        : IEntityReg<MetaTask, Id, int>
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
        public override MetaTask Register(MetaTask mt)
        {
            this.AssertNotNull(mt, "mt");
            try
            {
                this._statusRepo.GetById(mt.StatusId);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"MetaTask of ID {mt.Id.Get} has an invalid Status ID of {mt.StatusId.Get}.");
            }
            try
            {
                this._factionRepo.GetById(mt.FactionId);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"MetaTask of ID {mt.Id.Get} has an invalid Factionn ID of {mt.FactionId.Get}.");
            }
            return base.Register(mt);
        }
    }
}