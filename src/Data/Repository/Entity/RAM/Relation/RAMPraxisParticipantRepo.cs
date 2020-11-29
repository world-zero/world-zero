using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using WorldZero.Common.ValueObject.DTO.General.Generic;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="IPraxisParticipantRepo"/>
    public class RAMPraxisParticipantRepo
        : IRAMEntityRelationCntRepo
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
        protected override int GetRuleCount()
        {
            var a = new PraxisParticipant(new Id(3), new Id(2));
            return a.GetUniqueRules().Count;
        }

        public IEnumerable<Id> GetCharIdsByPraxisId(Id praxisId)
        {
            if (praxisId == null)
                throw new ArgumentNullException("playerId");

            IEnumerable<Id> charIds =
                from ppTemp in this._saved.Values
                let pp = this.TEntityCast(ppTemp)
                where pp.PraxisId == praxisId
                select pp.CharacterId;

            if (charIds.Count() == 0)
                throw new ArgumentException($"There are no characters associated with PraxisId of {praxisId.Get}");
            else
                return charIds;
        }

        public bool ParticipantCheck(Id praxisId, Id characterId)
        {
            if (praxisId == null)
                throw new ArgumentNullException("praxisId");
            if (characterId == null)
                throw new ArgumentNullException("characterId");

            IEnumerable<PraxisParticipant> participants =
                from ppTemp in this._saved.Values
                let pp = this.TEntityCast(ppTemp)
                where pp.PraxisId == praxisId
                where pp.CharacterId == characterId
                select pp;

            if (participants.Count() == 0)
                return false;

            return true;
        }
        
        public async Task<bool> ParticipantCheckAsync(Id praxisId, Id characterId)
        {
            return this.ParticipantCheck(praxisId, characterId);
        }

        public int GetParticipantCountViaPraxisId(Id praxisId)
        {
            if (praxisId == null)
                throw new ArgumentNullException("praxisId");

            IEnumerable<PraxisParticipant> participants =
                from ppTemp in this._saved.Values
                let pp = this.TEntityCast(ppTemp)
                where pp.PraxisId == praxisId
                select pp;

            return participants.Count();
        }

        public async Task<int> GetParticipantCountViaPraxisIdAsync(Id praxisId)
        {
            return this.GetParticipantCountViaPraxisId(praxisId);
        }

        public int GetParticipantCountViaPPId(Id ppId)
        {
            if (ppId == null)
                throw new ArgumentNullException("ppId");

            Id praxisId;
            try
            {
                praxisId = this.GetById(ppId).PraxisId;
            }
            catch (ArgumentException)
            { return 0; }

            return this.GetParticipantCountViaPraxisId(praxisId);
        }

        public async Task<int> GetParticipantCountViaPPIdAsync(Id ppId)
        {
            return this.GetParticipantCountViaPPId(ppId);
        }

        public
        IEnumerable<CountingDTO<Id>> GetParticipantCountsViaCharId(Id charId)
        {
            if (charId == null)
                throw new ArgumentNullException("charId");

            IEnumerable<Id> praxisIds =
                from ppTemp in this._saved.Values
                let pp = this.TEntityCast(ppTemp)
                where pp.CharacterId == charId
                select pp.PraxisId;

            foreach (Id praxisId in praxisIds)
            {
                int count = this.GetParticipantCountViaPraxisId(praxisId);
                yield return new CountingDTO<Id>(praxisId, count);
            }
        }

        public void DeleteByPraxisId(Id praxisId)
        {
            this.DeleteByLeftId(praxisId);
        }

        public async Task DeleteByPraxisIdAsync(Id praxisId)
        {
            this.DeleteByPraxisId(praxisId);
        }

        public void DeleteByCharacterId(Id charId)
        {
            this.DeleteByRightId(charId);
        }

        public async Task DeleteByCharacterIdAsync(Id charId)
        {
            this.DeleteByCharacterId(charId);
        }
    }
}