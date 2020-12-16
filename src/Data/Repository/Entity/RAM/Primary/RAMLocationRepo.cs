using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;

namespace WorldZero.Data.Repository.Entity.RAM.Primary
{
    /// <inheritdoc cref="ILocationRepo"/>
    public class RAMLocationRepo
        : IRAMIdEntityRepo<ILocation>,
        ILocationRepo
    {
        protected override int GetRuleCount()
        {
            var a = new UnsafeLocation(
                new Name("x"),
                new Name("or"),
                new Name("s"),
                new Name("97045")
            );
            return a.GetUniqueRules().Count;
        }
    }
}