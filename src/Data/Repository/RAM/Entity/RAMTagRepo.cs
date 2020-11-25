using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.RAM.Entity.Generic;

namespace WorldZero.Data.Repository.RAM.Entity
{
    /// <inheritdoc cref="ITagRepo"/>
    public class RAMTagRepo
        : IRAMNamedEntityRepo<Tag>,
        ITagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Tag(new Name("good"));
            return a.GetUniqueRules().Count;
        }
    }
}