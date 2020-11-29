using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.RAM.Entity;

namespace WorldZero.Data.Repository.RAM.Entity
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
    }
}