using System.Threading.Tasks;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.RAM.Generic;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="ITaskTagRepo"/>
    public class RAMTaskTagRepo
        : IRAMTaggedEntityRepo
          <
            TaskTag,
            Id,
            int,
            RelationDTO<Id, int, Name, string>
          >,
          ITaskTagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new TaskTag(new Id(3), new Name("d"));
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