using WorldZero.Service.Interface.Entity.Registration;
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
        : IEntityRelationReg
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
        protected IUnsafeTaskTagRepo _taskTagRepo
        { get { return (IUnsafeTaskTagRepo) this._repo; } }

        protected IUnsafeTaskRepo _taskRepo
        { get { return (IUnsafeTaskRepo) this._leftRepo; } }

        protected IUnsafeTagRepo _tagRepo
        { get { return (IUnsafeTagRepo) this._rightRepo; } }

        public TaskTagReg(
            IUnsafeTaskTagRepo tagTagRepo,
            IUnsafeTaskRepo taskRepo,
            IUnsafeTagRepo tagRepo
        )
            : base(tagTagRepo, taskRepo, tagRepo)
        { }
    }
}