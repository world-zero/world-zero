using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Unspecified.Relation;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="IMetaTaskFlagRepo"/>
    public class RAMMetaTaskFlagRepo
        : IRAMFlaggedEntityRepo
          <
            IMetaTaskFlag,
            Id,
            int,
            RelationDTO<Id, int, Name, string>
          >,
          IMetaTaskFlagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new UnsafeMetaTaskFlag(new Id(3), new Name("sdf"));
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