using WorldZero.Common.ValueObject;
using WorldZero.Data.Interface.Repository.Entity.RAM.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="ITaskTagRepo"/>
    public class RAMTaskTagRepo
        : IRAMIdNameRepo<TaskTag>,
          ITaskTagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new TaskTag(new Id(3), new Name("d"));
            return a.GetUniqueRules().Count;
        }
    }
}