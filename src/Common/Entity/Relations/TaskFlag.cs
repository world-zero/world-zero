using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="ITaskFlag"/>
    public sealed class TaskFlag
        : IUnsafeFlaggedProxy<UnsafeTaskFlag, Id, int>, ITaskFlag
    {
        public TaskFlag(UnsafeTaskFlag taskFlag)
            : base(taskFlag)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new TaskFlag(this._unsafeEntity);
        }

        public Id TaskId => this._unsafeEntity.TaskId;
    }
}