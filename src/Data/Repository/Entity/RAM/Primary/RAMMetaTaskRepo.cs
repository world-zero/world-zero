using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;

namespace WorldZero.Data.Repository.Entity.RAM.Primary
{
    /// <inheritdoc cref="IMetaTaskRepo"/>
    public class RAMMetaTaskRepo
        : IRAMIdStatusedEntityRepo<MetaTask>,
        IMetaTaskRepo
    {
        public IEnumerable<MetaTask> GetByFactionId(Name factionId)
        {
            if (factionId == null)
                throw new ArgumentNullException("factionId");
            
            IEnumerable<MetaTask> mts =
                from mtTemp in this._saved.Values
                let mt = this.TEntityCast(mtTemp)
                where mt.FactionId == factionId
                select mt;

            if (mts.Count() == 0)
                throw new ArgumentException($"There are no meta tasks from faction {factionId.Get}.");

            return mts;
        }

        protected override int GetRuleCount()
        {
            var a = new MetaTask(
                new Name("x"),
                new Name("tag"),
                "x",
                new PointTotal(1)
            );
            return a.GetUniqueRules().Count;
        }
    }
}