using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.RAM.Entity;

namespace WorldZero.Data.Repository.RAM.Entity
{
    /// <inheritdoc cref="IMetaTaskRepo"/>
    public class RAMMetaTaskRepo
        : IRAMIdEntityRepo<MetaTask>,
        IMetaTaskRepo
    {
        protected override int GetRuleCount()
        {
            var a = new MetaTask(new Name("x"), new Name("tag"), "x", 1);
            return a.GetUniqueRules().Count;
        }
    }
}