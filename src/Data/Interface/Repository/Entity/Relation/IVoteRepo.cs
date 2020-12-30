using System.Threading.Tasks;
using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface IVoteRepo
        : IEntityRelationRepo
          <
            IVote,
            Id,
            int,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
          >
    {
        /// <summary>
        /// Get a collection of Vote IDs that have a matching character ID. If
        /// there are none, then an exception is thrown.
        /// </summary>
        IEnumerable<Id> GetIdsByCharacterId(Id charId);

        /// <summary>
        /// Get a collection of Vote IDs that have a matching praxis
        /// participant ID. If there are none, then an exception is thrown.
        /// </summary>
        IEnumerable<Id> GetIdsByPraxisParticipantId(Id ppId);

        /// <summary>
        /// Get a collection of Character IDs that have a matching praxis
        /// participant ID with any ID in the supplied list. If there are none
        /// or the list is empty, then an exception is thrown.
        /// </summary>
        IEnumerable<Id> GetCharacterIdsByPraxisParticipantIds(List<Id> ppIds);

        /// <summary>
        /// `Delete()` all votes submitted by the supplied character ID. 
        /// </summary>
        void DeleteByCharacterId(Id charId);
        Task DeleteByCharacterIdAsync(Id charId);

        /// <summary>
        /// `Delete()` all votes associated with the supplied praxis
        /// participant ID. 
        /// </summary>
        void DeleteByPraxisParticipantId(Id praxisId);
        Task DeleteByPraxisParticipantIdAsync(Id praxisId);
    }
}