using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a praxis' ID to a character's ID, as well as
    /// containing the content of the comment they are leaving on the related
    /// praxis.
    /// </summary>
    public class Comment : IIdIdCntRelation
    {
        /// <summary>
        /// PraxisId wraps LeftId, which is the ID of the related Praxis.
        /// </summary>
        public Id PraxisId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        /// <summary>
        /// CharacterId wraps RightId, which is the ID of the related Character.
        /// </summary>
        public Id CharacterId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public Comment(
            Id praxisId,
            Id characterId,
            string comment,
            int count=1
        )
            : base(praxisId, characterId, count)
        {
            this.Value = comment;
        }

        public Comment(
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

        public Comment(
            CntRelationDTO<Id, int, Id, int> dto,
            string comment
        )
            : base(dto.LeftId, dto.RightId, dto.Count)
        {
            this.Value = comment;
        }

        public Comment(
            Id id,
            CntRelationDTO<Id, int, Id, int> dto,
            string comment
        )
            : base(id, dto.LeftId, dto.RightId, dto.Count)
        {
            this.Value = comment;
        }

        internal Comment(
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
            return new Comment(
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