using WorldZero.Common.Entity;

namespace WorldZero.Data.Interface.Repository.Entity
{
    /// <inheritdoc cref="INamedEntityRepo"/>
    /// <remarks>
    /// Unlike normal repositories, this repo will not be responsible for
    /// ensuring that there is only one active era and that eras do not
    /// overlap with one another (as these are internal consistency
    /// functionalities, which means the repo would be firmly responsible for
    /// them). This is passed to CreateEra as this functionality would better
    /// be implementation-independent, mostly because can you even imagine the
    /// SQL that you'd need to write to get this rule enforces?? We do not have
    /// a dedicated database admin so I am making an executive decision. As a
    /// result, nothing should alter IEraRepo directly, and instead use the
    /// CreateEra service class. This smell is tolerable since eras are sharply
    /// divorced from other repos.
    /// </remarks>
    public interface IEraRepo
        : INamedEntityRepo<Era>
    {
        /// <summary>
        /// Return the active era. If there is no active era, this returns
        /// null.
        /// </summary>
        /// <remarks>
        /// The current era must be saved to be considered active. This
        /// will not create a serialized transaction.
        /// </remarks>
        Era GetActiveEra();
    }
}