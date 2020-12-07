using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
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