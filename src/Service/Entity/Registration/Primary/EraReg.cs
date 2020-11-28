using System;
using WorldZero.Service.Interface.Entity.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;

namespace WorldZero.Service.Entity.Registration.Primary
{
    /// <inheritdoc cref="IEntityReg"/>
    /// <summary>
    /// This class is responsible for creating new eras.
    /// </summary>
    /// <remarks>
    /// If no era exists when an instance is initialized, this will create a
    /// default era. There should always be at least one Era at all times.
    /// <br />
    /// While `EraReg.Register(Era)` does perform the era roll-over, this does
    /// not perform any other era roll-over behavior, like moving all Active
    /// praxises to become Retired. This is because there is a specific flow
    /// that is defined by the requirements doc to avoid giving a
    /// non-interactive method too much changing power. The doc describes this
    /// functionality to live in `CzarConsole`.
    /// <br />
    /// As long as this class is the only item altering the supplied Era
    /// repository, there will be no internal consistency errors.
    /// <br />
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
        {
            this._eraRepo.BeginTransaction(true);
            if (this._eraRepo.GetActiveEra() == null)
            {
                this.Register(new Era(
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

            this._eraRepo.BeginTransaction(true);
            Era old = this._eraRepo.GetActiveEra();
            if (old != null)
            {
                old.EndDate = now;
                this._repo.Update(old);
            }
            newEra.EndDate = null;
            newEra.StartDate = now;
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
        public Era Register(Name newEraName)
        {
            return this.Register(
                new Era(newEraName, startDate: new PastDate(DateTime.UtcNow))
            );
        }

        // TODO: This shouldn't be on a creation registry; I have this logged
        // and I will move it later.
        /// <summary>
        /// Return the current era. For more, <see
        /// cref="WorldZero.Data.Interface.Repository.Entity.Primary.IEraRepo.GetActiveEra()"/>.
        /// </summary>
        /// <remarks>
        /// That said, `EraReg` will not allow a null era to be returned as
        /// this class should have created one during initialization if there
        /// was no active era.
        /// </remarks>
        public Era GetActiveEra()
        {
            var e = this._eraRepo.GetActiveEra();
            if (e == null)
                throw new InvalidOperationException("There is no active era found.");
            return e;
        }
    }
}