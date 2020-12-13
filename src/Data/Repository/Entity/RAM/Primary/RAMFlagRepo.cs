using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;

namespace WorldZero.Data.Repository.Entity.RAM.Primary
{
    /// <inheritdoc cref="IFlagRepo"/>
    public class RAMFlagRepo
        : IRAMNamedEntityRepo<IFlag>,
        IFlagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new UnsafeFlag(new Name("d"));
            return a.GetUniqueRules().Count;
        }
    }
}