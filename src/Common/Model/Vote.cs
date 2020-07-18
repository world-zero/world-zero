using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Model
{
    [Table("Vote")]
    /// <summary>
    /// Vote is a model for a tuple of the Vote table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class Vote : IModel
    {
        [Key, Column(Order=1)]
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
        /// <summary>
        /// The <c>Praxis</c> that this <c>Vote</c> is on.
        /// </summary>
        public virtual Praxis Praxis { get; set; }

        [Key, Column(Order=2)]
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
        /// <summary>
        /// The <c>Character</c> that submitted this <c>Vote</c>.
        /// </summary>
        public virtual Character Character { get; set; }

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