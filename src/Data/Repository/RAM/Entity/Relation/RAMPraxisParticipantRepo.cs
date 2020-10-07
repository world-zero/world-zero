using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.RAM.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.RAM.Entity.Relation
{
    /// <inheritdoc cref="IPraxisParticipantRepo"/>
    public class RAMPraxisParticipantRepo
        : IRAMEntityRelationRepo
          <
            PraxisParticipant,
            Id,
            int,
            Id,
            int,
            CntRelationDTO<Id, int, Id, int>
          >,
          IPraxisParticipantRepo
    {
        public IEnumerable<Id> GetCharIdsByPraxisId(Id praxisId)
        {
            if (praxisId == null)
                throw new ArgumentNullException("playerId");

            IEnumerable<Id> charIds =
                from praxisParticipant in this._saved.Values
                where praxisParticipant.PraxisId == praxisId
                select praxisParticipant.CharacterId;

            if (charIds.Count() == 0)
                throw new ArgumentException($"There are no characters associated with PraxisId of {praxisId.Get}");
            else
                return charIds;
        }

        protected override int GetRuleCount()
        {
            var a = new PraxisParticipant(new Id(3), new Id(2));
            return a.GetUniqueRules().Count;
        }
    }
}