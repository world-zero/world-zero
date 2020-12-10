using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="IFoe"/>
    public sealed class Foe
        : IUnsafeSelfRelationProxy<UnsafeFoe, Id, int>, IFoe
    {
        public Foe(UnsafeFoe foe)
            : base(foe)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new Foe(this._unsafeEntity);
        }

        public Id FirstCharacterId => this._unsafeEntity.FirstCharacterId;
        public Id SecondCharacterId => this._unsafeEntity.SecondCharacterId;
    }
}