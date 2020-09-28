using WorldZero.Common.ValueObject;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.RAM;

namespace WorldZero.Data.Repository.Entity.RAM
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