using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.RAM.Entity.Generic;

namespace WorldZero.Data.Repository.RAM.Entity.Primary
{
    /// <inheritdoc cref="IFlagRepo"/>
    public class RAMFlagRepo
        : IRAMNamedEntityRepo<Flag>,
        IFlagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Flag(new Name("d"));
            return a.GetUniqueRules().Count;
        }
    }
}