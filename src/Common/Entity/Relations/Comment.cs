using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="IComment"/>
    public sealed class Comment
        : IUnsafeIdIdCntRelationProxy<UnsafeComment>, IComment
    {
        public Comment(UnsafeComment comment)
            : base(comment)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new Comment(this._unsafeEntity);
        }

        public Id PraxisId => this._unsafeEntity.PraxisId;
        public Id CharacterId => this._unsafeEntity.CharacterId;
        public string Value => this._unsafeEntity.Value;
    }
}