using System;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Entity.Deletion.Relation;

namespace WorldZero.Service.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IEntityDel"/>
    /// <summary>
    /// As you migh expect, deleting a task will delete the Tags, Flags, and
    /// Praxises on that task - this involves using those corresponding
    /// deletion service classes.
    /// </summary>
    public class TaskDel : IIdStatusedEntityDel<Task>
    {
        protected ITaskRepo _taskRepo
        { get { return (ITaskRepo) this._repo; } }

        protected readonly TaskTagDel _taskTagDel;
        protected readonly TaskFlagDel _taskFlagDel;
        protected readonly PraxisDel _praxisDel;

        public TaskDel(
            ITaskRepo taskRepo,
            TaskTagDel taskTagDel,
            TaskFlagDel taskFlagDel,
            PraxisDel praxisDel
        )
            : base(taskRepo)
        {
            this.AssertNotNull(taskTagDel, "taskTagDel");
            this.AssertNotNull(taskFlagDel, "taskFlagDel");
            this.AssertNotNull(praxisDel, "praxisDel");

            this._taskTagDel = taskTagDel;
            this._taskFlagDel = taskFlagDel;
            this._praxisDel = praxisDel;
        }

        public override void Delete(Id taskId)
        {
            void op(Id taskId0)
            {
                this._taskTagDel.DeleteByTask(taskId);
                this._taskFlagDel.DeleteByTask(taskId);
                this._praxisDel.DeleteByTask(taskId);
                base.Delete(taskId0);
            }

            this.Transaction<Id>(op, taskId);
        }
    }
}