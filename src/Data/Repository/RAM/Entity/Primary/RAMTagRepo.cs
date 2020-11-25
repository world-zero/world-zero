using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.RAM.Entity.Generic;

namespace WorldZero.Data.Repository.RAM.Entity.Primary
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