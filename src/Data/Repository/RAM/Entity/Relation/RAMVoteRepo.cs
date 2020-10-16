using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Data.Interface.Repository.RAM.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Data.Repository.RAM.Entity.Relation
{
    /// <inheritdoc cref="IVoteRepo"/>
    public class RAMVoteRepo
        : IRAMEntityRelationRepo
          <
            Vote,
            Id,
            int,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
          >,
          IVoteRepo
    {
        public IEnumerable<Id> GetPraxisVoters(Id praxisId)
        {
            if (praxisId == null)
                throw new ArgumentNullException("playerId");

            IEnumerable<Id> charIds =
                from ppTemp in this._saved.Values
                let pp = this.TEntityCast(ppTemp)
                where pp.PraxisId == praxisId
                select pp.VotingCharacterId;

            if (charIds.Count() == 0)
                throw new ArgumentException($"There are no characters associated with PraxisId of {praxisId.Get}");
            else
                return charIds;
        }

        protected override int GetRuleCount()
        {
            var a =
                new Vote(new Id(2), new Id(2), new Id(3), new PointTotal(3));
            return a.GetUniqueRules().Count;
        }
    }
}