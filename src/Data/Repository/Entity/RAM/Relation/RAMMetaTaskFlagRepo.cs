using WorldZero.Common.ValueObject;
using WorldZero.Data.Interface.Repository.Entity.RAM.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="IMetaTaskFlagRepo"/>
    public class RAMMetaTaskFlagRepo
        : IRAMIdNameRepo<MetaTaskFlag>,
          IMetaTaskFlagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new MetaTaskFlag(new Id(3), new Name("sdf"));
            return a.GetUniqueRules().Count;
        }
    }
}