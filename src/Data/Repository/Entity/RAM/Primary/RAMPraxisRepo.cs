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
        : IRAMIdEntityRepo<Praxis>,
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

            string ppName = typeof(PraxisParticipant).Name;
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
                throw new ArgumentException($"There are no praxises with a meta task ID of {metaTaskId}");
            else
                return praxises;
        }
    }
}