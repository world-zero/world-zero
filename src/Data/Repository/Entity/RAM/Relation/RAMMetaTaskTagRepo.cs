using WorldZero.Common.ValueObject;
using WorldZero.Data.Interface.Repository.Entity.RAM.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="IMetaTaskTagRepo"/>
    public class RAMMetaTaskTagRepo
        : IRAMIdNameRepo<MetaTaskTag>,
          IMetaTaskTagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new MetaTaskTag(new Id(3), new Name("sdf"));
            return a.GetUniqueRules().Count;
        }
    }
}