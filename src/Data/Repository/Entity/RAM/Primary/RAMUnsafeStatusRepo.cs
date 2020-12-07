using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;

namespace WorldZero.Data.Repository.Entity.RAM.Primary
{
    /// <inheritdoc cref="IUnsafeStatusRepo"/>
    public class RAMUnsafeStatusRepo
        : IRAMNamedEntityRepo<UnsafeStatus>,
        IUnsafeStatusRepo
    {
        protected override int GetRuleCount()
        {
            var a = new UnsafeStatus(new Name("good"));
            return a.GetUniqueRules().Count;
        }
    }
}