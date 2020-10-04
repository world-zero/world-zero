using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Registration.Entity.Relation
{
    public class TaskFlagReg
        : IEntityRelationReg
        <
            TaskFlag,
            Task,
            Id,
            int,
            Flag,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >
    {
        protected ITaskFlagRepo _taskFlagRepo
        { get { return (ITaskFlagRepo) this._repo; } }

        protected ITaskRepo _taskRepo
        { get { return (ITaskRepo) this._leftRepo; } }

        protected IFlagRepo _flagRepo
        { get { return (IFlagRepo) this._rightRepo; } }

        public TaskFlagReg(
            ITaskFlagRepo taskFlagRepo,
            ITaskRepo taskRepo,
            IFlagRepo flagRepo
        )
            : base(taskFlagRepo, taskRepo, flagRepo)
        { }
    }
}