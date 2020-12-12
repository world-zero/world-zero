using WorldZero.Service.Interface.Entity.Generic.Registration;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Service.Entity.Registration.Relation
{
    /// <inheritdoc cref="IEntityRelationReg"/>
    public class TaskTagReg
        : ABCEntityRelationReg
        <
            UnsafeTaskTag,
            UnsafeTask,
            Id,
            int,
            UnsafeTag,
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