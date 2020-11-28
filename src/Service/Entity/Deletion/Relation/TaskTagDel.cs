using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Relation
{
    /// <inheritdoc cref="ITaggedEntityDel"/>
    public class TaskTagDel : ITaggedEntityDel
    <
        TaskTag,
        Task,
        Id,
        int,
        RelationDTO<Id, int, Name, string>
    >
    {
        public TaskTagDel(ITaskTagRepo repo)
            : base(repo)
        { }

        /// <remarks>
        /// This is just a wrapper for `DeleteByLeftId()`.
        /// </remarks>
        public void DeleteByTask(Task task)
        {
            this.AssertNotNull(task, "task");
            this.DeleteByLeft(task.Id);
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByLeftId()`.
        /// </remarks>
        public void DeleteByTask(Id taskId)
        {
            this.AssertNotNull(taskId, "taskId");
            this.DeleteByLeft(taskId);
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByLeftIdAsync()`.
        /// </remarks>
        public async
        System.Threading.Tasks.Task DeleteByTaskAsync(Task task)
        {
            this.AssertNotNull(task, "task");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByLeft(task.Id));
        }

        /// <remarks>
        /// This is just a wrapper for `DeleteByLeftIdAsync()`.
        /// </remarks>
        public async
        System.Threading.Tasks.Task DeleteByTaskAsync(Id taskId)
        {
            this.AssertNotNull(taskId, "taskId");
            await System.Threading.Tasks.Task.Run(() =>
                this.DeleteByLeft(taskId));
        }
    }
}