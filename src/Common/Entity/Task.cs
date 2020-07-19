using System;
using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Entity
{
    [Table("Task")]
    /// <summary>
    /// Task is a entity for a tuple of the Task table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class Task : IEntity
    {
        [Required]
        /// <summary>
        /// Summary is a string summary of a task.
        /// </summary>
        public string Summary { get; set; }

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

        [Required]
        /// <summary>
        /// Level is a wrapper for a <c>Level</c> - no exceptions are
        /// caught.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// This is thrown when Level is set to be lower than MinLevel.
        /// </exception>
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
        private Level _level;

        [Required]
        /// <summary>
        /// MinLevel is a wrapper for a <c>Level</c> - no exceptions are
        /// caught.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// This is thrown when MinLevel is set to be higher than Level.
        /// </exception>
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
        [NotMapped]
        private Level _minLevel;

        private void _checkLevels(Level high, Level low)
        {
            if ( (high != null)
                && (low != null)
                && (low.Get > high.Get) )
            {
                throw new ArgumentException("The Level must be at least MinLevel.");
            }
        }

        [Required]
        /// <summary>
        /// FactionId is a wrapper for an <c>Id</c> - no exceptions are
        /// caught.
        /// </summary>
        public virtual int FactionId
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._factionId,
                    0);
            }
            set { this._factionId = new Id(value); }
        }
        [NotMapped]
        private Id _factionId;

        [ForeignKey("FactionId")]
        internal virtual Faction Faction { get; set; }

        [Required]
        /// <summary>
        /// StatusId is a wrapper for an <c>Id</c> - no exceptions are
        /// caught.
        /// </summary>
        public virtual int StatusId
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._statusId,
                    0);
            }
            set { this._statusId = new Id(value); }
        }
        [NotMapped]
        private Id _statusId;

        [ForeignKey("StatusId")]
        internal virtual Status Status { get; set; }

        internal virtual ICollection<Tag> Tags { get; set; }
        internal virtual ICollection<Flag> Flags { get; set; }
        internal virtual ICollection<Praxis> Praxises { get; set; }
    }
}