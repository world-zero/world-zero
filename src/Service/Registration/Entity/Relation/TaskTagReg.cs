using WorldZero.Service.Interface.Registration.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Entity;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Registration.Entity.Relation
{
    public class TaskTagReg
        : IEntityRelationReg
        <
            TaskTag,
            Task,
            Id,
            int,
            Tag,
            Name,
            string,
            RelationDTO<Id, int, Name, string>
        >
    {
        protected ITaskTagRepo _taskTagRepo
        { get { return (ITaskTagRepo) this._repo; } }

        protected ITaskRepo _taskRepo
        { get { return (ITaskRepo) this._leftRepo; } }

        protected ITagRepo _tagRepo
        { get { return (ITagRepo) this._rightRepo; } }

        public TaskTagReg(
            ITaskTagRepo tagTagRepo,
            ITaskRepo taskRepo,
            ITagRepo tagRepo
        )
            : base(tagTagRepo, taskRepo, tagRepo)
        { }
    }
}