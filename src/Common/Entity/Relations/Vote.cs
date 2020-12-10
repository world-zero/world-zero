using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="IVote"/>
    public sealed class Vote
        : IUnsafeIdIdRelationProxy<UnsafeVote>, IVote
    {
        public Vote(UnsafeVote vote)
            : base(vote)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new Vote(this._unsafeEntity);
        }

        public PointTotal Points => this._unsafeEntity.Points;
        public Id CharacterId => this._unsafeEntity.CharacterId;
        public Id PraxisParticipantId =>this._unsafeEntity.PraxisParticipantId;
        public PointTotal StaticMinPoints =>this._unsafeEntity.StaticMinPoints;
        public PointTotal StaticMaxPoints =>this._unsafeEntity.StaticMaxPoints;
    }
}