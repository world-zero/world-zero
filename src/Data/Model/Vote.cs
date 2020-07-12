using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Vote")]
    public class Vote : IModel
    {
        [NotMapped]
        private Id _praxisId;
        [Key, Column(Order=1)]
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
        [ForeignKey("PraxisId")]
        public virtual Praxis Praxis { get; set; }

        [NotMapped]
        private Id _characterId;
        [Key, Column(Order=2)]
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
        [ForeignKey("CharacterId")]
        public virtual Character Character { get; set; }

        [NotMapped]
        private PointTotal _points;
        [Required]
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
    }
}