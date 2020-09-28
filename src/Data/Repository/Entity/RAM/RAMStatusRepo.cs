using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.RAM;

namespace WorldZero.Data.Repository.Entity.RAM
{
    /// <inheritdoc cref="IStatusRepo"/>
    public class RAMStatusRepo
        : IRAMNamedEntityRepo<Status>,
        IStatusRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Status(new Name("good"));
            return a.GetUniqueRules().Count;
        }
    }
}