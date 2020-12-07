using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using WorldZero.Common.ValueObject.DTO.General.Generic;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
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
            RelationDTO<Id, int, Id, int>
          >,
          IPraxisParticipantRepo
    {
        public IEnumerable<PraxisParticipant> GetByPraxisId(Id praxisId)
        {
            return this.GetByLeftId(praxisId);
        }

        public IEnumerable<PraxisParticipant> GetByCharacterId(Id charId)
        {
            return this.GetByRightId(charId);
        }

        public IEnumerable<Id> GetIdsByPraxisId(Id praxisId)
        {
            return this.GetIdsByLeftId(praxisId);
        }

        public IEnumerable<Id> GetIdsByCharacterId(Id charId)
        {
            return this.GetIdsByRightId(charId);
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

        public async
        System.Threading.Tasks.Task DeleteByPraxisIdAsync(Id praxisId)
        {
            this.DeleteByPraxisId(praxisId);
        }

        public void DeleteByCharacterId(Id charId)
        {
            this.DeleteByRightId(charId);
        }

        public async
        System.Threading.Tasks.Task DeleteByCharacterIdAsync(Id charId)
        {
            this.DeleteByCharacterId(charId);
        }

        protected override int GetRuleCount()
        {
            var a = new PraxisParticipant(new Id(3), new Id(2));
            return a.GetUniqueRules().Count;
        }

        public int GetPraxisCount(Id characterId, ISet<Name> statuses)
        {
            if (characterId == null)
                throw new ArgumentNullException("characterId");
            if (statuses == null)
                throw new ArgumentNullException("statuses");
            if (statuses.Count == 0)
                return 0;

            string praxisName = typeof(UnsafePraxis).FullName;
            if (!_data.ContainsKey(praxisName))
                return 0;

            IEnumerable<UnsafePraxis> praxises =
                from pTemp in _data[praxisName].Saved.Values
                let p = (UnsafePraxis) pTemp
                where statuses.Contains(p.StatusId)

                from ppTemp in this._saved.Values
                let pp = this.TEntityCast(ppTemp)

                where pp.CharacterId == characterId
                where pp.PraxisId == p.Id
                select p;

            return praxises.Count();
        }

        public async System.Threading.Tasks.Task<int> GetPraxisCountAsync(
            Id characterId,
            ISet<Name> statuses
        )
        {
            return this.GetPraxisCount(characterId, statuses);
        }

        public int GetCharacterSubmissionCount(Id taskId, Id charId)
        {
            if (taskId == null)
                throw new ArgumentNullException("taskId");
            if (charId == null)
                throw new ArgumentNullException("charId");

            if (!_data.ContainsKey(typeof(UnsafePraxis).FullName))
                return 0;

            var pEntityData = _data[typeof(UnsafePraxis).FullName];
            IEnumerable<Id> results =
                from pTemp in pEntityData.Saved.Values
                let p = (UnsafePraxis) pTemp
                where p.TaskId == taskId
                from ppTemp in this._saved.Values
                let pp = this.TEntityCast(ppTemp)
                where pp.PraxisId == p.Id
                where pp.CharacterId == charId
                select p.Id;

           return results.Count();
        }

        public async System.Threading.Tasks.Task<int>
        GetCharacterSubmissionCountAsync(Id taskId, Id charId)
        {
            return this.GetCharacterSubmissionCount(taskId, charId);
        }

        public int GetCharacterSubmissionCountViaPraxisId(Id praxisId, Id charId)
        {
            if (praxisId == null)
                throw new ArgumentNullException("praxisId");
            if (charId == null)
                throw new ArgumentNullException("charId");

            if (!_data.ContainsKey(typeof(UnsafePraxis).FullName))
                return 0;

            var pEntityData = _data[typeof(UnsafePraxis).FullName];
            IEnumerable<Id> taskId =
                from pTemp in pEntityData.Saved.Values
                let p = (UnsafePraxis) pTemp
                where p.Id == praxisId
                select p.TaskId;

            int c = taskId.Count();
            if (c == 0)
                return 0;
            else if (c > 1)
                throw new InvalidOperationException("There should not be more than one Task, but several were found.");

           return this.GetCharacterSubmissionCount(taskId.First(), charId);
        }

        public async System.Threading.Tasks.Task<int>
        GetCharacterSubmissionCountViaPraxisIdAsync(Id praxisId, Id charId)
        {
            return this.GetCharacterSubmissionCountViaPraxisId(praxisId, charId);
        }
    }
}