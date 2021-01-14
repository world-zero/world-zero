using System.Threading.Tasks;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="ITaskTagRepo"/>
    public class RAMTaskTagRepo
        : IRAMTaggedEntityRepo
          <
            ITaskTag,
            Id,
            int,
            NoIdRelationDTO<Id, int, Name, string>
          >,
          ITaskTagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new UnsafeTaskTag(new Id(3), new Name("d"));
            return a.GetUniqueRules().Count;
        }

        public void DeleteByTaskId(Id taskId)
        {
            this.DeleteByLeftId(taskId);
        }

        public async Task DeleteByTaskIdAsync(Id taskId)
        {
            this.DeleteByTaskId(taskId);
        }
    }
}