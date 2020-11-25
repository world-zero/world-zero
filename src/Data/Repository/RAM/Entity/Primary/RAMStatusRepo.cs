using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.RAM.Entity.Generic;

namespace WorldZero.Data.Repository.RAM.Entity.Primary
{
    /// <inheritdoc cref="IStatusRepo"/>
    public class RAMStatusRepo
        : IRAMNamedEntityRepo<Status>,
        IStatusRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Status(new Name("good"));
            return a.GetUniqueRules().Count;
        }
    }
}