using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="ITag"/>
    public sealed class Tag
        : IUnsafeNamedProxy<UnsafeTag>, ITag
    {
        public Tag(UnsafeTag tag)
            : base(tag)
        { }

        public override IEntity<Name, string> Clone()
        {
            return new Tag(this._unsafeEntity);
        }

        public string Description => this._unsafeEntity.Description;
    }
}