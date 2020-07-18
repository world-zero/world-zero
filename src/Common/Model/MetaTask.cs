using System;
using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Model
{
    [Table("MetaTask")]
    /// <summary>
    /// MetaTask is a model for a tuple of the MetaTask table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class MetaTask : IModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// MetaTaskId is a wrapper for an <c>Id</c> - no exceptions are
        /// caught.
        /// </summary>
        public int MetaTaskId
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._metaTaskId,
                    0);
            }
            set { this._metaTaskId = new Id(value); }
        }
        [NotMapped]
        private Id _metaTaskId;

        [Required]
        /// <summary>
        /// MetaTaskName is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
        public string MetaTaskName
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._metaTaskName,
                    null);
            }
            set { this._metaTaskName = new Name(value); }
        }
        [NotMapped]
        private Name _metaTaskName;

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
        /// FactionName is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
        public virtual string FactionName
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._factionName,
                    null);
            }
            set { this._factionName = new Name(value); }
        }
        [NotMapped]
        private Name _factionName;

        [ForeignKey("FactionName")]
        /// <summary>
        /// The <c>Faction</c> that this <c>MetaTask</c> belongs to.
        /// </summary>
        public virtual Faction Faction { get; set; }

        // Pretend this is required.
        /// <summary>
        /// StatusName is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
        public virtual string StatusName
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._statusName,
                    null);
            }
            set { this._statusName = new Name(value); }
        }
        [NotMapped]
        private Name _statusName;

        [ForeignKey("StatusName")]
        /// <summary>
        /// The <c>Status</c> that this <c>MetaTask</c> has.
        /// </summary>
        public virtual Status Status { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Flag> Flags { get; set; }
        public virtual ICollection<Praxis> Praxises { get; set; }
    }
}