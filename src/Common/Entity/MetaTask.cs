using System;
using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Entity
{
    [Table("MetaTask")]
    /// <summary>
    /// MetaTask is a entity for a tuple of the MetaTask table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class MetaTask : IIdEntity
    {
        [Required]
        /// <summary>
        /// Description is a description of the MetaTask.
        /// </summary>
        public string Description { get; set; }

        [Required]
        /// <summary>
        /// Bonus is a non-negative double that can be either a flag point
        /// bonus or a point percentage modifier. For more, see
        /// <see cref="MetaTask.IsFlatBonus"/>.
        /// </summary>
        public double Bonus
        {
            get { return this._bonus; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("A bonus must be a positive number.");
                this._bonus = value;
            }
        }
        [NotMapped]
        private double _bonus;

        [Required]
        /// <summary>
        /// IsFlatBonus determines whether <c>Bonus</c> is a flat bonus point
        /// addition or if it is a point percentage modifier.
        /// </summary>
        public bool IsFlatBonus { get; set; } = true;

        [Required]
        /// <summary>
        /// FactionId is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
        public virtual string FactionId
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._factionId,
                    null);
            }
            set { this._factionId = new Name(value); }
        }
        [NotMapped]
        private Name _factionId;

        [ForeignKey("FactionId")]
        internal virtual Faction Faction { get; set; }

        // Pretend this is required.
        /// <summary>
        /// StatusId is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
        public virtual string StatusId
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._statusId,
                    null);
            }
            set
            {
                if (value == null) this._statusId = null;
                else               this._statusId = new Name((string) value);
            }
        }
        [NotMapped]
        private Name _statusId;

        [ForeignKey("StatusId")]
        internal virtual Status Status { get; set; }

        internal virtual ICollection<Tag> Tags { get; set; }
        internal virtual ICollection<Flag> Flags { get; set; }
        internal virtual ICollection<Praxis> Praxises { get; set; }
    }
}