using System.Threading.Tasks;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Interface.Entity.Deletion.Relation;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="ITaskFlagDel"/>
    public class TaskFlagDel : ABCFlaggedEntityDel
    <
        ITaskFlag,
        ITask,
        Id,
        int,
        NoIdRelationDTO<Id, int, Name, string>
    >, ITaskFlagDel
    {
        public TaskFlagDel(ITaskFlagRepo repo)
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