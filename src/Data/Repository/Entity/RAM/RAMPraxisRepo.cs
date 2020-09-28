using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.RAM;

namespace WorldZero.Data.Repository.Entity.RAM
{
    /// <inheritdoc cref="IPraxisRepo"/>
    public class RAMPraxisRepo
        : IRAMIdEntityRepo<Praxis>,
        IPraxisRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Praxis(new Id(2), new Name("f"));
            return a.GetUniqueRules().Count;
        }
    }
}