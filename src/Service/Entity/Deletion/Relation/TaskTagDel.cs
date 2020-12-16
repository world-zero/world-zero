using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Interface.Entity.Deletion.Relation;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="ITaskTagDel"/>
    public class TaskTagDel : ABCTaggedEntityDel
    <
        ITaskTag,
        ITask,
        Id,
        int,
        RelationDTO<Id, int, Name, string>
    >, ITaskTagDel
    {
        public TaskTagDel(ITaskTagRepo repo)
            : base(repo)
        { }

        public void DeleteByTask(ITask task)
        {
            this.AssertNotNull(task, "task");
            this.DeleteByLeft(task.Id);
        }

        public void DeleteByTask(Id taskId)
        {
            this.AssertNotNull(taskId, "taskId");
            this.DeleteByLeft(taskId);
        }

        public async Task DeleteByTaskAsync(ITask task)
        {
            this.AssertNotNull(task, "task");
            await Task.Run(() => this.DeleteByLeft(task.Id));
        }

        public async Task DeleteByTaskAsync(Id taskId)
        {
            this.AssertNotNull(taskId, "taskId");
            await Task.Run(() => this.DeleteByLeft(taskId));
        }
    }
}