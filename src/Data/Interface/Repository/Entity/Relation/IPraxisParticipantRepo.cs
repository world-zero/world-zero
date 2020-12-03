using System.Threading.Tasks;
using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.General.Generic;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface IPraxisParticipantRepo
        : IEntityRelationRepo
          <
            PraxisParticipant,
            Id,
            int,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
          >
    {
        IEnumerable<PraxisParticipant> GetByPraxisId(Id praxisId);
        IEnumerable<PraxisParticipant> GetByCharacterId(Id characterId);

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