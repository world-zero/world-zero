using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="IPraxisFlag"/>
    public sealed class PraxisFlag
        : IUnsafeFlaggedProxy<UnsafePraxisFlag, Id, int>, IPraxisFlag
    {
        public PraxisFlag(UnsafePraxisFlag praxisFlag)
            : base(praxisFlag)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new PraxisFlag(this._unsafeEntity);
        }

        public Id PraxisId => this._unsafeEntity.PraxisId;
    }
}