using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.RAM.Entity;

namespace WorldZero.Data.Repository.RAM.Entity
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