using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Registration;

namespace WorldZero.Service.Interface.Entity.Registration.Primary
{
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
    /// <inheritdoc cref="IEntityReg{TEntity, TId, TBuiltIn}"/>
    public interface IEraReg
        : IEntityReg<IEra, Name, string>
    {
        // TODO: This shouldn't be on a creation registry; I have this logged
        // and I will move it later. This exists on EraReg too.
        /// <summary>
        /// Return the current era. For more, <see
        /// cref="WorldZero.Data.Interface.Repository.Entity.Primary.IEraRepo.GetActiveEra()"/>.
        /// </summary>
        /// <remarks>
        /// That said, `EraReg` will not allow a null era to be returned as
        /// this class should have created one during initialization if there
        /// was no active era.
        /// </remarks>
        IEra GetActiveEra();
    }
}