using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="ITaskTag"/>
    public sealed class TaskTag
        : IUnsafeTaggedProxy<UnsafeTaskTag, Id, int>, ITaskTag
    {
        public TaskTag(UnsafeTaskTag taskTag)
            : base(taskTag)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new TaskTag(this._unsafeEntity);
        }

        public Id TaskId => this._unsafeEntity.TaskId;
    }
}