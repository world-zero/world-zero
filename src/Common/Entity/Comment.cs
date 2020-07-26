using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Entity
{
    [Table("Comment")]
    /// <summary>
    /// Comment is a entity for a tuple of the Comment table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class Comment : IIdEntity
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
        internal virtual Character Character { get; set; }

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
                // This is done because if a creation date isn't set, it can't
                // be later than the first time it is being accessed, so it is
                // set.
                DateTime val = this.Eval<DateTime>(
                    (ISingleValueObject<DateTime>) this._dateCreated,
                    DateTime.UtcNow);
                this._dateCreated = new PastDate(val);
                return val;
            }
            set { this._dateCreated = new PastDate(value); }
        }
        [NotMapped]
        private PastDate _dateCreated;

        internal virtual ICollection<Flag> Flags { get; set; }
    }
}