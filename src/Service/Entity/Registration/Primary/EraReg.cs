using System;
using WorldZero.Service.Interface.Entity.Registration.Primary;
using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

// TODO: fix this class - I have this logged

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEraReg"/>
    public class EraReg
        : ABCEntityReg<IEra, Name, string>, IEraReg
    {
        public EraReg(IEraRepo eraRepo)
            : base(eraRepo)
        {
            this._eraRepo.BeginTransaction(true);
            if (this._eraRepo.GetActiveEra() == null)
            {
                this.Register(new UnsafeEra(
                    new Name("The Beginning"),
                    startDate: new PastDate(DateTime.UtcNow))
                );
                this._eraRepo.EndTransaction();
            }
            this._eraRepo.DiscardTransaction();
        }

        protected IEraRepo _eraRepo { get { return (IEraRepo) this._repo; } }

        /// <summary>
        /// This method will end the previous era and begin the supplied era.
        /// This will set newEra's start date to now and set the end date to
        /// null.
        /// </summary>
        /// <remarks>
        /// This will not effect anything outside of the supplied era repo.
        /// <br />
        /// Changing the outlined date controlling may result in a corruptable
        /// Era repository as these are the only safeguards in place to prevent
        /// non-overlapping eras and no more than one era being active at once.
        /// In fact, at the time of writing, this will strictly enforce only
        /// one active era at any given point in time (after the initial era
        /// has been started).
        /// </remarks>
        public override IEra Register(IEra newEra)
        {
            this.AssertNotNull(newEra, "newEra");
            var now = new PastDate(DateTime.UtcNow);

            this._eraRepo.BeginTransaction(true);
            UnsafeEra old = (UnsafeEra) this._eraRepo.GetActiveEra();
            if (old != null)
            {
                old.EndDate = now;
                this._repo.Update(old);
            }
            ((UnsafeEra) newEra).EndDate = null;
            ((UnsafeEra) newEra).StartDate = now;
            try
            {
                var r = base.Register(newEra);
                this._eraRepo.EndTransaction();
                return r;
            }
            catch (ArgumentException exc)
            {
                this._eraRepo.DiscardTransaction();
                throw new ArgumentException("Could not complete the registration.", exc);
            }
        }

        /// <summary>
        /// This method acts the same as CreateEra.Register(Era), but it does
        /// not require a new Era to be created since every part of it but the
        /// name is altered anyways.
        /// </summary>
        public IEra Register(Name newEraName)
        {
            return this.Register(
                new UnsafeEra(newEraName, startDate: new PastDate(DateTime.UtcNow))
            );
        }

        // TODO: This shouldn't be on a creation registry; I have this logged
        // and I will move it later. This exists on IEraReg too.
        public IEra GetActiveEra()
        {
            var e = this._eraRepo.GetActiveEra();
            if (e == null)
                throw new InvalidOperationException("There is no active era found.");
            return e;
        }
    }
}