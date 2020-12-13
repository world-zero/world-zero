using System.Threading.Tasks;
using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.General.Generic;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface IPraxisParticipantRepo
        : IEntityRelationRepo
          <
            IPraxisParticipant,
            Id,
            int,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
          >
    {
        /// <summary>
        /// This will return the number of praxises a character has with
        /// statuses that exist in `statuses`. This will return zero if there
        /// are no statuses in the set, or if the needed information about
        /// praxises could not be retrieved.
        /// </returns>
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
        /// This is extremely similar to <see cref=
        /// "IPraxisParticipantRepo.GetCharacterSubmissionCount(Id, Id)"/>,
        /// but this method will take a praxisId as the first argument and
        /// determine the corresponding taskId as an intermediary step.
        /// </summary>
        int GetCharacterSubmissionCountViaPraxisId(Id praxisId, Id charId);
        Task<int> GetCharacterSubmissionCountViaPraxisIdAsync(
            Id praxisId,
            Id charId
        );

        IEnumerable<IPraxisParticipant> GetByPraxisId(Id praxisId);
        IEnumerable<IPraxisParticipant> GetByCharacterId(Id characterId);

        /// <summary>
        /// Get a collection of PraxisParticipant IDs that are participants of
        /// the supplied praxis ID. If there are none, then an exception is
        /// thrown.
        /// </summary>
        IEnumerable<Id> GetIdsByPraxisId(Id praxisId);

        /// <summary>
        /// Get a collection of PraxisParticipant IDs that are participants of
        /// the supplied character ID. If there are none, then an exception is
        /// thrown.
        /// </summary>
        IEnumerable<Id> GetIdsByCharacterId(Id characterId);

        /// <summary>
        /// Get a collection of saved PraxisParticipant.CharacterId's that have
        /// the supplied PraxisId. If there are none, then an exception is thrown.
        /// </summary>
        IEnumerable<Id> GetCharIdsByPraxisId(Id praxisId);

        /// <summary>
        /// This will check if the supplied praxis ID has the supplied
        /// participant and return a boolean accordingly.
        /// </summary>
        bool ParticipantCheck(Id praxisId, Id characterId);
        Task<bool> ParticipantCheckAsync(Id praxisId, Id characterId);

        /// <summary>
        /// Return the number of participants associated with a praxis.
        /// </summary>
        int GetParticipantCountViaPraxisId(Id praxisId);
        Task<int> GetParticipantCountViaPraxisIdAsync(Id praxisId);

        /// <summary>
        /// Return the number of participants that participate on the same
        /// praxis as praxis participant `ppId`. If `ppId` does not exist, this
        /// will return 0.
        /// </summary>
        int GetParticipantCountViaPPId(Id ppId);
        Task<int> GetParticipantCountViaPPIdAsync(Id ppId);

        /// <summary>
        /// This returns an iterable of `CountingDTO`s, where each DTO's
        /// `Countee` is the Id of a Praxis, and the `Count` is the number of
        /// participants present for that praxis.
        /// </summary>
        /// <remarks>
        /// Basically, this is an iterable version (that takes a character ID)
        /// of <see cref="WorldZero.Data.Interface.Repository.Entity.Relation.IPraxisParticipantRepo.GetParticipantCountViaPraxisId(Id)"/>.
        /// </remarks>
        IEnumerable<CountingDTO<Id>> GetParticipantCountsViaCharId(Id characterId);

        /// <summary>
        /// `Delete()` all PraxisParticipants with the supplied praxis ID.
        /// </summary>
        void DeleteByPraxisId(Id praxisId);
        Task DeleteByPraxisIdAsync(Id praxisId);

        /// <summary>
        /// `Delete()` all PraxisParticipants with the supplied character ID.
        /// </summary>
        void DeleteByCharacterId(Id characterId);
        Task DeleteByCharacterIdAsync(Id characterId);
    }
}