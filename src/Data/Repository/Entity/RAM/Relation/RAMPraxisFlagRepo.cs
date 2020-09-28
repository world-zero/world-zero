using WorldZero.Common.ValueObject;
using WorldZero.Data.Interface.Repository.Entity.RAM.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="IPraxisFlagRepo"/>
    public class RAMPraxisFlagRepo
        : IRAMIdNameRepo<PraxisFlag>,
          IPraxisFlagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new PraxisFlag(new Id(3), new Name("sdf"));
            return a.GetUniqueRules().Count;
        }
    }
}