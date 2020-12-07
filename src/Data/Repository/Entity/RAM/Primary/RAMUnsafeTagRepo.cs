using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;

namespace WorldZero.Data.Repository.Entity.RAM.Primary
{
    /// <inheritdoc cref="IUnsafeTagRepo"/>
    public class RAMUnsafeTagRepo
        : IRAMNamedEntityRepo<UnsafeTag>,
        IUnsafeTagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new UnsafeTag(new Name("good"));
            return a.GetUniqueRules().Count;
        }
    }
}