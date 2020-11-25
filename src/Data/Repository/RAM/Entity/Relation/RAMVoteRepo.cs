using System;
using System.Linq;
using System.Threading.Tasks;
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
        protected override int GetRuleCount()
        {
            var a =
                new Vote(new Id(2), new Id(2), new Id(3), new PointTotal(3));
            return a.GetUniqueRules().Count;
        }

        public IEnumerable<Id> GetPraxisVoters(Id praxisId)
        {
            if (praxisId == null)
                throw new ArgumentNullException("playerId");

            IEnumerable<Id> charIds =
                from voteTemp in this._saved.Values
                let vote = this.TEntityCast(voteTemp)
                where vote.PraxisId == praxisId
                select vote.VotingCharacterId;

            if (charIds.Count() == 0)
                throw new ArgumentException($"There are no characters associated with PraxisId of {praxisId.Get}");
            else
                return charIds;
        }

        public void DeleteByVotingCharId(Id charId)
        {
            this.DeleteByLeftId(charId);
        }

        public async Task DeleteByVotingCharIdAsync(Id charId)
        {
            this.DeleteByVotingCharId(charId);
        }

        public void DeleteByPraxisId(Id praxisId)
        {
            this.DeleteByReceivingCharId(praxisId);
        }

        public async Task DeleteByPraxisIdAsync(Id praxisId)
        {
            this.DeleteByPraxisId(praxisId);
        }

        public void DeleteByReceivingCharId(Id charId)
        {
            if (charId == null)
                throw new ArgumentNullException("playerId");

            IEnumerable<Id> charIds =
                from voteTemp in this._saved.Values
                let vote = this.TEntityCast(voteTemp)
                where vote.ReceivingCharacterId == charId
                select vote.Id;

            foreach (Id id in charIds)
                this.Delete(id);
        }

        public async Task DeleteByReceivingCharIdAsync(Id charId)
        {
            this.DeleteByReceivingCharId(charId);
        }
    }
}