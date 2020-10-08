using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Interface.Repository.Entity.Relation
{
    public interface IVoteRepo
        : IEntityRelationRepo
          <
            Vote,
            Id,
            int,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
          >
    {
        /// <summary>
        /// Get a collection of saved character IDs that have voted on the
        /// supplied praxis. If there are none, then an exception is thrown.
        /// </summary>
        IEnumerable<Id> GetPraxisVoters(Id praxisId);
    }
}