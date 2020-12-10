using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="IStatus"/>
    public sealed class Status
        : IUnsafeNamedProxy<UnsafeStatus>, IStatus
    {
        public Status(UnsafeStatus status)
            : base(status)
        { }

        public override IEntity<Name, string> Clone()
        {
            return new Status(this._unsafeEntity);
        }

        public string Description => this._unsafeEntity.Description;
    }
}