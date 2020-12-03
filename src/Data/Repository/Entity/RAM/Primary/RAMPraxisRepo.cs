using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;

namespace WorldZero.Data.Repository.Entity.RAM.Primary
{
    /// <inheritdoc cref="IPraxisRepo"/>
    public class RAMPraxisRepo
        : IRAMIdStatusedEntityRepo<Praxis>,
        IPraxisRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Praxis(new Id(2), new PointTotal(2), new Name("f"));
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

            string ppName = typeof(PraxisParticipant).FullName;
            if (!_data.ContainsKey(ppName))
                throw new InvalidOperationException("A repo for PraxisParticipants has not been created.");

            IEnumerable<Praxis> praxises =
                from ppTemp in _data[ppName].Saved.Values
                let pp = (PraxisParticipant) ppTemp

                from pTemp in this._saved.Values
                let p = this.TEntityCast(pTemp)

                where pp.CharacterId == characterId
                where pp.PraxisId == p.Id
                where statuses.Contains(p.StatusId)
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

            if (!_data.ContainsKey(typeof(PraxisParticipant).FullName))
                // There isn't even a RAMPraxisParticipantRepo instance, so the
                // Character couldn't have already submitted a praxis since
                // they couldn't have registered it as their praxis.
                return 0;

            var ppEntityData = _data[typeof(PraxisParticipant).FullName];
            IEnumerable<Id> results =
                from pTemp in this._saved.Values
                let p = this.TEntityCast(pTemp)
                where p.TaskId == taskId
                from ppTemp in ppEntityData.Saved.Values
                let pp = (PraxisParticipant) ppTemp
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

            if (!_data.ContainsKey(typeof(PraxisParticipant).FullName))
                // There isn't even a RAMPraxisParticipantRepo instance, so the
                // Character couldn't have already submitted a praxis since
                // they couldn't have registered it as their praxis.
                return 0;

            IEnumerable<Id> taskId =
                from pTemp in this._saved.Values
                let p = this.TEntityCast(pTemp)
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

        public IEnumerable<Praxis> GetByMetaTaskId(Id metaTaskId)
        {
            if (metaTaskId == null)
                throw new ArgumentNullException("metaTaskId");

            IEnumerable<Praxis> praxises =
                from p in this._saved.Values
                let praxis = this.TEntityCast(p)
                where praxis.MetaTaskId != null
                where praxis.MetaTaskId == metaTaskId
                select praxis;

            if (praxises.Count() == 0)
                throw new ArgumentException($"There are no praxises with a meta task ID of {metaTaskId.Get}");
            else
                return praxises;
        }

        public IEnumerable<Praxis> GetByTaskId(Id taskId)
        {
            if (taskId == null)
                throw new ArgumentNullException("taskId");

            IEnumerable<Praxis> praxises =
                from p in this._saved.Values
                let praxis = this.TEntityCast(p)
                where praxis.TaskId == taskId
                select praxis;

            if (praxises.Count() == 0)
                throw new ArgumentException($"There are no praxises with a task ID of {taskId.Get}");
            else
                return praxises;
        }
    }
}