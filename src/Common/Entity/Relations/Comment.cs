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

        public Comment(Id praxisId, Id characterId, string comment)
            : base(praxisId, characterId)
        {
            this.Value = comment;
        }

        public Comment(Id id, Id praxisId, Id characterId, string comment)
            : base(id, praxisId, characterId)
        {
            this.Value = comment;
        }

        public Comment(IdIdDTO dto, string comment)
            : base(dto.LeftId, dto.RightId)
        {
            this.Value = comment;
        }

        public Comment(Id id, IdIdDTO dto, string comment)
            : base(id, dto.LeftId, dto.RightId)
        {
            this.Value = comment;
        }

        internal Comment(int id, int praxisId, int characterId, string comment)
            : base(new Id(id), new Id(praxisId), new Id(characterId))
        {
            this.Value = comment;
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
    }
}