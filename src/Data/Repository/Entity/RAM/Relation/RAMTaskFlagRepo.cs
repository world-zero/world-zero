using WorldZero.Common.ValueObject;
using WorldZero.Data.Interface.Repository.Entity.RAM.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="ITaskFlagRepo"/>
    public class RAMTaskFlagRepo
        : IRAMIdNameRepo<TaskFlag>,
          ITaskFlagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new TaskFlag(new Id(3), new Name("d"));
            return a.GetUniqueRules().Count;
        }
    }
}