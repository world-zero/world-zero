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
        [Key, Column(Order=1)]
        public virtual int PraxisId { get; set; }
        [ForeignKey("PraxisId")]
        public virtual PraxisModel Praxis { get; set; }

        [Key, Column(Order=2)]
        public virtual int CharacterId { get; set; }
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

        public virtual ICollection<FlagModel> Flags { get; set; }
    }
}