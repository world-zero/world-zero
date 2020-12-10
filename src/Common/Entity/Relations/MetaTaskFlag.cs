using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="IMetaTaskFlag"/>
    public sealed class MetaTaskFlag
        : IUnsafeFlaggedProxy<UnsafeMetaTaskFlag, Id, int>, IMetaTaskFlag
    {
        public MetaTaskFlag(UnsafeMetaTaskFlag mtFlag)
            : base(mtFlag)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new MetaTaskFlag(this._unsafeEntity);
        }

        public Id MetaTaskId => this._unsafeEntity.MetaTaskId;
    }
}