using WorldZero.Common.ValueObject;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.RAM;

namespace WorldZero.Data.Repository.Entity.RAM
{
    /// <inheritdoc cref="ILocationRepo"/>
    public class RAMLocationRepo
        : IRAMIdEntityRepo<Location>,
        ILocationRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Location(
                new Name("x"),
                new Name("or"),
                new Name("s"),
                new Name("97045")
            );
            return a.GetUniqueRules().Count;
        }
    }
}