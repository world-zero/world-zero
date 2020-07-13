using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Comment")]
    /// <summary>
    /// Comment is a model for a tuple of the Comment table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class Comment : IModel
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
        /// The <c>Praxis</c> that this <c>Comment</c> is attached to.
        /// </summary>
        public virtual Praxis Praxis { get; set; }

        [Key, Column(Order=2)]
        /// <summary>
        /// PraxisId is a wrapper for an <c>Id</c> - no exceptions are
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
        /// The <c>Character</c> that this <c>Comment</c> is made by.
        /// </summary>
        public virtual Character Character { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        /// <summary>
        /// DateCreated is a wrapper for a <c>PastDate</c> - no exceptions are
        /// caught.
        /// </summary>
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
        [NotMapped]
        private PastDate _dateCreated;

        public virtual ICollection<Flag> Flags { get; set; }
    }
}