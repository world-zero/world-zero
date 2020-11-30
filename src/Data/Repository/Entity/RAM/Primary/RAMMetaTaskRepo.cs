using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;

namespace WorldZero.Data.Repository.Entity.RAM.Primary
{
    /// <inheritdoc cref="IMetaTaskRepo"/>
    public class RAMMetaTaskRepo
        : IRAMIdStatusedEntityRepo<MetaTask>,
        IMetaTaskRepo
    {
        protected override int GetRuleCount()
        {
            var a = new MetaTask(
                new Name("x"),
                new Name("tag"),
                "x",
                new PointTotal(1)
            );
            return a.GetUniqueRules().Count;
        }
    }
}