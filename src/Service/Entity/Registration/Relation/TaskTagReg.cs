using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Service.Interface.Entity.Registration.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="ITaskTagReg"/>
    public class TaskTagReg
        : ABCEntityRelationReg
        <
            ITaskTag,
            ITask,
            Id,
            int,
            ITag,
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