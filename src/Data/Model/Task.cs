using System;
using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Task")]
    public class Task : IModel
    {
        [NotMapped]
        private Id _taskId;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._taskId,
                    0);
            }
            set { this._taskId = new Id(value); }
        }

        [Required]
        public string Summary { get; set; }
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

        [NotMapped]
        private Level _level;
        [Required]
        public int Level
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._level,
                    0);
            }
            set
            {
                var l = new Level(value);
                this._checkLevels(l, this._minLevel);
                this._level = l;
            }
        }

        [NotMapped]
        private Level _minLevel;
        [Required]
        public int MinLevel
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._minLevel,
                    0);
            }
            set
            {
                var l = new Level(value);
                this._checkLevels(this._level, l);
                this._minLevel = l;
            }
        }

        private void _checkLevels(Level high, Level low)
        {
            if ( (high != null)
                && (low != null)
                && (low.Get > high.Get) )
            {
                throw new ArgumentException("The Level must be at least MinLevel.");
            }
        }

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
        [Required]
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
        public virtual Status Status { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Flag> Flags { get; set; }
        public virtual ICollection<Praxis> Praxises { get; set; }
    }
}