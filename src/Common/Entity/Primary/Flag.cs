using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="IFlag"/>
    public sealed class Flag : IUnsafeNamedProxy<UnsafeFlag>, IFlag
    {
        public Flag(UnsafeFlag flag)
            : base(flag)
        { }

        public override IEntity<Name, string> Clone()
        {
            return new Flag(this._unsafeEntity);
        }

        public string Description => this._unsafeEntity.Description;
        public bool IsFlatPenalty => this._unsafeEntity.IsFlatPenalty;
        public PointTotal Penalty => this._unsafeEntity.Penalty;
    }
}