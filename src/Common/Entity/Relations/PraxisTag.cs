using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="IPraxisTag"/>
    public sealed class PraxisTag
        : IUnsafeTaggedProxy<UnsafePraxisTag, Id, int>, IPraxisTag
    {
        public PraxisTag(UnsafePraxisTag praxisTag)
            : base(praxisTag)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new PraxisTag(this._unsafeEntity);
        }

        public Id PraxisId => this._unsafeEntity.PraxisId;
    }
}