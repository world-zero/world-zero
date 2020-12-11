using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;

namespace WorldZero.Data.Repository.Entity.RAM.Primary
{
    /// <inheritdoc cref="IPlayerRepo"/>
    public class RAMPlayerRepo
        : IRAMIdNamedEntityRepo<UnsafePlayer>,
        IPlayerRepo
    {
        protected override int GetRuleCount()
        {
            var a = new UnsafePlayer(new Name("Hal"));
            return a.GetUniqueRules().Count;
        }
    }
}