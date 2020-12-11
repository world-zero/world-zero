using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;

namespace WorldZero.Data.Repository.Entity.RAM.Primary
{
    /// <inheritdoc cref="ITaskRepo"/>
    public class RAMTaskRepo
        : IRAMIdStatusedEntityRepo<UnsafeTask>,
        ITaskRepo
    {
        public IEnumerable<UnsafeTask> GetByFactionId(Name factionId)
        {
            if (factionId == null)
                throw new ArgumentNullException("factionId");
            
            IEnumerable<UnsafeTask> tasks =
                from taskTemp in this._saved.Values
                let t = this.TEntityCast(taskTemp)
                where t.FactionId == factionId
                select t;

            if (tasks.Count() == 0)
                throw new ArgumentException($"There are no tasks from faction {factionId.Get}.");

            return tasks;
        }

        protected override int GetRuleCount()
        {
            var a = new UnsafeTask(
                new Name("x"),
                new Name("f"),
                "x",
                new PointTotal(2),
                new Level(2)
            );
            return a.GetUniqueRules().Count;
        }
    }
}