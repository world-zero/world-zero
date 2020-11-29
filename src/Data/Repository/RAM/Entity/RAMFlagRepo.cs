using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Entity;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.RAM.Entity;

namespace WorldZero.Data.Repository.RAM.Entity
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