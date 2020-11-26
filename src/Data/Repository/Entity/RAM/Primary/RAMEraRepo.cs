using System;
using System.Linq;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;

namespace WorldZero.Data.Repository.Entity.RAM.Primary
{
    /// <inheritdoc cref="IEraRepo"/>
    public class RAMEraRepo
        : IRAMNamedEntityRepo<Era>,
        IEraRepo
    {
        public Era GetActiveEra()
        {
            var active = from e in this._saved.Values
                let era = this.TEntityCast(e)
                where era.EndDate == null
                select era;

            if (active.Count() == 0)
                return null;

            else if (active.Count() == 1)
                return active.First();

            else
                throw new InvalidOperationException("There should not be more than one active era at a time, the repo as been populated incorrectly.");
        }

        public async System.Threading.Tasks.Task<Era> GetActiveEraAsync()
        {
            return this.GetActiveEra();
        }

        protected override int GetRuleCount()
        {
            var a = new Era(new Name("s"));
            return a.GetUniqueRules().Count;
        }
    }
}