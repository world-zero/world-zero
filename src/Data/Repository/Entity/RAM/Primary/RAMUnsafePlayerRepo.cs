using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;

namespace WorldZero.Data.Repository.Entity.RAM.Primary
{
    /// <inheritdoc cref="IUnsafePlayerRepo"/>
    public class RAMUnsafePlayerRepo
        : IRAMIdNamedEntityRepo<UnsafePlayer>,
        IUnsafePlayerRepo
    {
        protected override int GetRuleCount()
        {
            var a = new UnsafePlayer(new Name("Hal"));
            return a.GetUniqueRules().Count;
        }
    }
}