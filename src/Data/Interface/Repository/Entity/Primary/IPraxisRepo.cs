using System.Collections.Generic;
using System.Threading.Tasks;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Primary
{
    /// <inheritdoc cref="IIdEntityRepo"/>
    public interface IPraxisRepo
        : IIdStatusedEntityRepo<Praxis>
    {
        /// <summary>
        /// This will return the number of praxises a character has with
        /// statuses that exist in `statuses`.
        /// </summary>
        /// <returns>
        /// This will return 0 if there are no statuses in the set.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// This is thrown if the related table for `PraxisParticipant`s cannot
        /// be found.
        /// </exception>
        int GetPraxisCount(Id charId, ISet<Name> statuses);
        Task<int> GetPraxisCountAsync(Id charId, ISet<Name> statuses);

        /// <summary>
        /// Return the number of praxises the character has submitted for the
        /// supplied task. If either argument does not exist, this will return
        /// zero.
        /// </summary>
        int GetCharacterSubmissionCount(Id taskId, Id charId);
        Task<int> GetCharacterSubmissionCountAsync(Id taskId, Id charId);

        /// <summary>
        /// This is extremely similar to <see
        /// cref="IPraxisRepo.GetCharacterSubmissionCount(Id, Id)"/>,
        /// but this method will take a praxisId as the first argument and
        /// determine the corresponding taskId as an intermediary step.
        /// </summary>
        int GetCharacterSubmissionCountViaPraxisId(Id praxisId, Id charId);
        Task<int> GetCharacterSubmissionCountViaPraxisIdAsync(
            Id praxisId,
            Id charId
        );

        /// <summary>
        /// Get a collection of saved Praxises that have a matching
        /// MetaTask.Id as the argument. If there are none, an exception is
        /// thrown.
        /// </summary>
        IEnumerable<Praxis> GetByMetaTaskId(Id metaTaskId);

        /// <summary>
        /// Get a collection of saved Praxises that are praxises of the task of
        /// the supplied ID. If there are none, an exception is thrown.
        /// </summary>
        IEnumerable<Praxis> GetByTaskId(Id taskId);
    }
}