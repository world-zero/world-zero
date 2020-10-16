using System;
using System.Linq;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.RAM.Entity;

namespace WorldZero.Data.Repository.RAM.Entity
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

        protected override int GetRuleCount()
        {
            var a = new Era(new Name("s"), new PastDate(DateTime.UtcNow));
            return a.GetUniqueRules().Count;
        }
    }
}