using System;
using WorldZero.Service.Interface;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Entity.Registration
{
    public class MetaTaskRegistration
        : IEntityRegistration<MetaTask, Id, int>
    {
        protected IMetaTaskRepo _metaTaskRepo
        { get { return (IMetaTaskRepo) this._repo; } }

        protected readonly IFactionRepo _factionRepo;
        protected readonly IStatusRepo _statusRepo;

        public MetaTaskRegistration(
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
            this.AssertNotNull(mt);
            try
            {
                this._statusRepo.GetById(mt.StatusId);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"MetaTask of ID {mt.Id} has an invalid Status ID of {mt.StatusId}.");
            }
            try
            {
                this._factionRepo.GetById(mt.FactionId);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"MetaTask of ID {mt.Id} has an invalid Factionn ID of {mt.FactionId}.");
            }
            return base.Register(mt);
        }
    }
}