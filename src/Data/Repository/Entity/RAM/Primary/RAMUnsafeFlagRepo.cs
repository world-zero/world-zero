using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;

namespace WorldZero.Data.Repository.Entity.RAM.Primary
{
    /// <inheritdoc cref="IUnsafeFlagRepo"/>
    public class RAMUnsafeFlagRepo
        : IRAMNamedEntityRepo<UnsafeFlag>,
        IUnsafeFlagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new UnsafeFlag(new Name("d"));
            return a.GetUniqueRules().Count;
        }
    }
}