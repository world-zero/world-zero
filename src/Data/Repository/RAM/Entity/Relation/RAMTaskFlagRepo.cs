using System.Threading.Tasks;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.RAM.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.RAM.Entity.Relation
{
    /// <inheritdoc cref="ITaskFlagRepo"/>
    public class RAMTaskFlagRepo
        : IRAMFlaggedEntityRepo
          <
            TaskFlag,
            Id,
            int,
            RelationDTO<Id, int, Name, string>
          >,
          ITaskFlagRepo
    {
        protected override int GetRuleCount()
        {
            var a = new TaskFlag(new Id(3), new Name("d"));
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