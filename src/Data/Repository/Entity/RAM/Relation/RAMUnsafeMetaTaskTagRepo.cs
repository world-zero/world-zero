using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="IUnsafeMetaTaskTagRepo"/>
    public class RAMUnsafeMetaTaskTagRepo
        : IRAMTaggedEntityRepo
          <
            UnsafeMetaTaskTag,
            Id,
            int,
            RelationDTO<Id, int, Name, string>
          >,
          IUnsafeMetaTaskTagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new UnsafeMetaTaskTag(new Id(3), new Name("sdf"));
            return a.GetUniqueRules().Count;
        }

        public void DeleteByMetaTaskId(Id mtId)
        {
            this.DeleteByLeftId(mtId);
        }

        public async Task DeleteByMetaTaskIdAsync(Id mtId)
        {
            this.DeleteByLeftId(mtId);
        }
    }
}