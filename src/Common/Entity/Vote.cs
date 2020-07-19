using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Entity
{
    [Table("Vote")]
    /// <summary>
    /// Vote is a entity for a tuple of the Vote table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class Vote : IEntity
    {
        [Required]
        /// <summary>
        /// PraxisId is a wrapper for an <c>Id</c> - no exceptions are
        /// caught.
        /// </summary>
        public virtual int PraxisId
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._praxisId,
                    0);
            }
            set { this._praxisId = new Id(value); }
        }
        [NotMapped]
        private Id _praxisId;

        [ForeignKey("PraxisId")]
        internal virtual Praxis Praxis { get; set; }

        [Required]
        /// <summary>
        /// CharacterId is a wrapper for an <c>Id</c> - no exceptions are
        /// caught.
        /// </summary>
        public virtual int CharacterId
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._characterId,
                    0);
            }
            set { this._characterId = new Id(value); }
        }
        [NotMapped]
        private Id _characterId;

        [ForeignKey("CharacterId")]
        internal virtual Character Character { get; set; }

        [Required]
        /// <summary>
        /// Points is a wrapper for a <c>PointTotal</c> - no exceptions are
        /// caught.
        /// </summary>
        public int Points
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._points,
                    0);
            }
            set { this._points = new PointTotal(value); }
        }
        [NotMapped]
        private PointTotal _points;
    }
}