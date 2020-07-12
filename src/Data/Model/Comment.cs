using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Comment")]
    public class Comment : IModel
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

        [Required]
        public string Value { get; set; }

        [NotMapped]
        private PastDate _dateCreated;
        [Required]
        public DateTime DateCreated
        {
            get
            {
                return this.Eval<DateTime>(
                    (ISingleValueObject<DateTime>) this._dateCreated,
                    DateTime.UtcNow);
            }
            set { this._dateCreated = new PastDate(value); }
        }

        public virtual ICollection<Flag> Flags { get; set; }
    }
}