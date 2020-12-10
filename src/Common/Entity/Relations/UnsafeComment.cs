using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Generic.Relation;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="IComment"/>
    public class UnsafeComment : IUnsafeIdIdCntRelation, IComment
    {
        public Id PraxisId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        public Id CharacterId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public UnsafeComment(
            Id praxisId,
            Id characterId,
            string comment,
            int count=1
        )
            : base(praxisId, characterId, count)
        {
            this.Value = comment;
        }

        public UnsafeComment(
            Id id,
            Id praxisId,
            Id characterId,
            string comment,
            int count=1
        )
            : base(id, praxisId, characterId, count)
        {
            this.Value = comment;
        }

        public UnsafeComment(
            CntRelationDTO<Id, int, Id, int> dto,
            string comment
        )
            : base(dto.LeftId, dto.RightId, dto.Count)
        {
            this.Value = comment;
        }

        public UnsafeComment(
            Id id,
            CntRelationDTO<Id, int, Id, int> dto,
            string comment
        )
            : base(id, dto.LeftId, dto.RightId, dto.Count)
        {
            this.Value = comment;
        }

        internal UnsafeComment(
            int id,
            int praxisId,
            int characterId,
            string comment,
            int count
        )
            : base(new Id(id), new Id(praxisId), new Id(characterId))
        {
            this.Value = comment;
            this.Count = count;
        }

        public override IEntity<Id, int> Clone()
        {
            return new UnsafeComment(
                this.Id,
                this.LeftId,
                this.RightId,
                this.Value,
                this.Count
            );
        }

        public string Value
        {
            get { return this._value; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Value cannot contain a null/empty string.");
                this._value = value;
            }
        }
        protected string _value;
    }
}