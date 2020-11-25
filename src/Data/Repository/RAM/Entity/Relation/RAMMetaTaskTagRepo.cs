using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Data.Interface.Repository.RAM.Entity.Generic;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.RAM.Entity.Relation
{
    /// <inheritdoc cref="IMetaTaskTagRepo"/>
    public class RAMMetaTaskTagRepo
        : IRAMTaggedEntityRepo
          <
            MetaTaskTag,
            Id,
            int,
            RelationDTO<Id, int, Name, string>
          >,
          IMetaTaskTagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new MetaTaskTag(new Id(3), new Name("sdf"));
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