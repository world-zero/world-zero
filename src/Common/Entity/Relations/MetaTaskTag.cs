using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="IMetaTaskTag"/>
    public sealed class MetaTaskTag
        : IUnsafeTaggedProxy<UnsafeMetaTaskTag, Id, int>, IMetaTaskTag
    {
        public MetaTaskTag(UnsafeMetaTaskTag mtTag)
            : base(mtTag)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new MetaTaskTag(this._unsafeEntity);
        }

        public Id MetaTaskId => this._unsafeEntity.MetaTaskId;
    }
}