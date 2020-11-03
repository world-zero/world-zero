using System;
using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Service.Registration.Entity
{
    /// <summary>
    /// This class is responsible for creating new eras. As long as this class
    /// is the only item altering the supplied Era repository, there will be no
    /// internal consistency errors.
    /// </summary>
    /// <remarks>
    /// Unlike other repositories, IEraRepo implementations are NOT responsible
    /// for ensuring that the eras are internally correct (in the sense that
    /// eras cannot overlap one another and that there can only be one active
    /// era at a time). Instead, for ease of development, those
    /// responsibilities are shifted to this service class. As a result, other
    /// classes should not use an IEraRepo directly. That said, IEraRepo is
    /// pretty sharply divorced from other repositories, which is why this
    /// smell is tolerable.
    /// </remarks>
    public class EraReg
        : IEntityReg<Era, Name, string>
    {
        public EraReg(IEraRepo eraRepo)
            : base(eraRepo)
        { }

        protected IEraRepo _eraRepo { get { return (IEraRepo) this._repo; } }

        /// <summary>
        /// This method will end the previous era and begin the supplied era.
        /// This will set newEra's start date to now and set the end date to
        /// null.
        /// 
        /// This will not effect anything outside of the supplied era repo.
        /// </summary>
        /// <remarks>
        /// Changing the outlined date controlling may result in a corruptable
        /// Era repository as these are the only safeguards in place to prevent
        /// non-overlapping eras and no more than one era being active at once.
        /// In fact, at the time of writing, this will strictly enforce only
        /// one active era at any given point in time (after the initial era
        /// has been started).
        /// </remarks>
        public override Era Register(Era newEra)
        {
            this.AssertNotNull(newEra, "newEra");
            var now = new PastDate(DateTime.UtcNow);

            this._eraRepo.BeginTransaction();
            Era old = this._eraRepo.GetActiveEra();
            if (old != null)
            {
                old.EndDate = now;
                this._repo.Update(old);
            }
            newEra.EndDate = null;
            newEra.StartDate = now;
            var r = base.Register(newEra);
            this._eraRepo.EndTransaction();

            return r;
        }

        /// <summary>
        /// This method acts the same as CreateEra.Register(Era), but it does
        /// not require a new Era to be created since every part of it but the
        /// name is altered anyways.
        /// </summary>
        public Era Register(Name newEraName)
        {
            return this.Register(
                new Era(newEraName, new PastDate(DateTime.UtcNow))
            );
        }
    }
}