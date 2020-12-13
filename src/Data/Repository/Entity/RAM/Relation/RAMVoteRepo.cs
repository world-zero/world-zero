using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="IVoteRepo"/>
    public class RAMVoteRepo
        : IRAMEntityRelationRepo
          <
            IVote,
            Id,
            int,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
          >,
          IVoteRepo
    {
        public IEnumerable<Id> GetIdsByCharacterId(Id charId)
        {
            return this.GetIdsByLeftId(charId);
        }

        public IEnumerable<Id> GetIdsByPraxisParticipantId(Id ppId)
        {
            return this.GetIdsByRightId(ppId);
        }

        public
        IEnumerable<Id> GetCharacterIdsByPraxisParticipantIds(List<Id> ppIds)
        {
            if (ppIds == null)
                throw new ArgumentNullException("praxisParticipantIds");

            if (ppIds.Count == 0)
                throw new ArgumentException("There are no supplied IDs.");

            var results = new List<Id>();
            foreach (Id ppId in ppIds)
            {
                try
                {
                    foreach (Id id in this.GetIdsByPraxisParticipantId(ppId))
                    {
                        results.Add(this.GetById(id).CharacterId);
                    }
                }
                catch (ArgumentException)
                { }
            }

            if (results.Count == 0)
                throw new ArgumentException("There are no IDs returned for the supplied IDs.");

            return results;
        }

        protected override int GetRuleCount()
        {
            var a = new UnsafeVote(
                new Id(2),
                new Id(2),
                new Id(3),
                new PointTotal(3));
            return a.GetUniqueRules().Count;
        }

        public void DeleteByCharacterId(Id charId)
        {
            this.DeleteByLeftId(charId);
        }

        public async Task DeleteByCharacterIdAsync(Id charId)
        {
            this.DeleteByCharacterId(charId);
        }

        public void DeleteByPraxisParticipantId(Id praxisId)
        {
            this.DeleteByRightId(praxisId);
        }

        public async Task DeleteByPraxisParticipantIdAsync(Id praxisId)
        {
            this.DeleteByPraxisParticipantId(praxisId);
        }
    }
}