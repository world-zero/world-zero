using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity.Relation;

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
            CntRelationDTO<Id, int, Id, int>
          >
    {
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

        /// <summary>
        /// Return tne number of participants associated with a praxis.
        /// </summary>
        int GetParticipantCount(Id praxisId);
    }
}