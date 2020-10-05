using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Registration.Entity.Relation
{
    public class TaskStatusReg
        : IEntityRelationReg
        <
            TaskStatus,
            Task,
            Id,
            int,
            Status,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >
    {
        protected ITaskStatusRepo _taskStatusRepo
        { get { return (ITaskStatusRepo) this._repo; } }

        protected ITaskRepo _taskRepo
        { get { return (ITaskRepo) this._leftRepo; } }

        protected IStatusRepo _statusRepo
        { get { return (IStatusRepo) this._rightRepo; } }

        public TaskStatusReg(
            ITaskStatusRepo taskStatusRepo,
            ITaskRepo taskRepo,
            IStatusRepo statusRepo
        )
            : base(taskStatusRepo, taskRepo, statusRepo)
        { }
    }
}