using System;
using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("MetaTask")]
    public class MetaTask : IModel
    {
        [NotMapped]
        private Id _metaTaskId;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        private Name _metaTaskName;
        [Required]
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

        [Required]
        public string Description { get; set; }

        [NotMapped]
        private double _bonus;
        [Required]
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

        [Required]
        public bool IsFlatBonus { get; set; } = true;

        [NotMapped]
        private Name _factionName;
        [Required]
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
        [ForeignKey("FactionName")]
        public virtual Faction Faction { get; set; }

        [NotMapped]
        private Name _statusName;
        // Pretend this is required.
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
        [ForeignKey("StatusName")]
        public virtual StatusModel Status { get; set; }

        public virtual ICollection<TagModel> Tags { get; set; }
        public virtual ICollection<Flag> Flags { get; set; }
        public virtual ICollection<Praxis> Praxises { get; set; }
    }
}