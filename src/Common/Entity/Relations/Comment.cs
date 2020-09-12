using System;
using WorldZero.Common.ValueObject;
using WorldZero.Common.DTO.Dual;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a praxis' ID to a character's ID, as well as
    /// containing the content of the comment they are leaving on the related
    /// praxis.
    /// </summary>
    /// </remarks>
    /// Comment repositories are responsible for ensuring that there
    /// is a triple uniqueness on PraxisId, CharacterId, and Count.
    /// </remarks>
    public class Comment : IIdIdRelation
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
        /// CharacterId wraps LeftId, which is the ID of the related Character.
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
            : base(praxisId, characterId)
        {
            this.Value = comment;
            this.Count = count;
        }

        public Comment(
            Id id,
            Id praxisId,
            Id characterId,
            string comment,
            int count=1
        )
            : base(id, praxisId, characterId)
        {
            this.Value = comment;
            this.Count = count;
        }

        public Comment(
            IdIdDTO dto,
            string comment,
            int count=1
        )
            : base(dto.LeftId, dto.RightId)
        {
            this.Value = comment;
            this.Count = count;
        }

        public Comment(
            Id id,
            IdIdDTO dto,
            string comment,
            int count=1
        )
            : base(id, dto.LeftId, dto.RightId)
        {
            this.Value = comment;
            this.Count = count;
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

        public override IEntity<Id, int> DeepCopy()
        {
            return new Comment(
                this.Id,
                this.LeftId,
                this.RightId,
                this.Value
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

        /// <summary>
        /// This value tracks the number of this comment that this character
        /// has submitted to the related praxis. This is necessary to allow
        /// characters to comment several times on a single praxis.
        /// </summary>
        public int Count
        {
            get { return this._count; }
            set
            {
                if (value < 1)
                    throw new ArgumentException("A character cannot start counting comments left on a praxis before 1.");
                this._count = value;
            }
        }
        protected int _count;
    }
}