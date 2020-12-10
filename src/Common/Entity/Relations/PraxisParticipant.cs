using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="IPraxisParticipant"/>
    public sealed class PraxisParticipant
        : IUnsafeIdIdRelationProxy<UnsafePraxisParticipant>, IPraxisParticipant
    {
        public PraxisParticipant(UnsafePraxisParticipant praxisParticipant)
            : base(praxisParticipant)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new PraxisParticipant(this._unsafeEntity);
        }

        public Id PraxisId => this._unsafeEntity.PraxisId;
        public Id CharacterId => this._unsafeEntity.CharacterId;
    }
}