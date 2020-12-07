using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="IUnsafeMetaTaskFlagRepo"/>
    public class RAMUnsafeMetaTaskFlagRepo
        : IRAMFlaggedEntityRepo
          <
            UnsafeMetaTaskFlag,
            Id,
            int,
            RelationDTO<Id, int, Name, string>
          >,
          IUnsafeMetaTaskFlagRepo
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