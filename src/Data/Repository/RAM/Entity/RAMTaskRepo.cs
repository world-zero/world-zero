using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.RAM.Entity;

namespace WorldZero.Data.Repository.RAM.Entity
{
    /// <inheritdoc cref="ITaskRepo"/>
    public class RAMTaskRepo
        : IRAMIdEntityRepo<Task>,
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