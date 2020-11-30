using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;

namespace WorldZero.Data.Repository.Entity.RAM.Primary
{
    /// <inheritdoc cref="ITaskRepo"/>
    public class RAMTaskRepo
        : IRAMIdStatusedEntityRepo<Task>,
        ITaskRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Task(
                new Name("x"),
                new Name("f"),
                "x",
                new PointTotal(2),
                new Level(2)
            );
            return a.GetUniqueRules().Count;
        }
    }
}