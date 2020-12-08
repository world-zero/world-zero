using System;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Entity.Primary
{
    /// <summary>
    /// Task is a entity for a tuple of the Task table.
    /// </summary>
    public class UnsafeTask : UnsafeIIdStatusedEntity, IUnsafeEntity
    {
        public UnsafeTask(
            Name factionId,
            Name statusId,
            string summary,
            PointTotal points,
            Level level,
            Level minLevel=null,
            bool isHistorianable=false
        )
            : base(statusId)
        {
            this._setup(
                summary,
                level,
                minLevel,
                points,
                factionId,
                isHistorianable
            );
        }

        public UnsafeTask(
            Id id,
            Name factionId,
            Name statusId,
            string summary,
            PointTotal points,
            Level level,
            Level minLevel=null,
            bool isHistorianable=false
        )
            : base(id, statusId)
        {
            this._setup(
                summary,
                level,
                minLevel,
                points,
                factionId,
                isHistorianable
            );
        }

        internal UnsafeTask(
            int id,
            string factionId,
            string statusId,
            string summary,
            int points,
            int level,
            int minLevel,
            bool isHistorianable
        )
            : base(new Id(id), new Name(statusId))
        {
            this._setup(
                summary,
                new Level(level),
                new Level(minLevel),
                new PointTotal(points),
                new Name(factionId),
                isHistorianable
            );
        }

        private void _setup(
            string summary,
            Level level,
            Level minLevel,
            PointTotal points,
            Name factionId,
            bool isHistorianable
        )
        {
            this.Summary = summary;
            this.Level = level;
            if (minLevel == null) this.MinLevel = new Level(0);
            else                  this.MinLevel = minLevel;
            this.Points = points;
            this.isHistorianable = isHistorianable;
            this.FactionId = factionId;
        }

        public override IEntity<Id, int> Clone()
        {
            return new UnsafeTask(
                this.Id,
                this.FactionId,
                this.StatusId,
                this.Summary,
                this.Points,
                this.Level,
                this.MinLevel,
                this.isHistorianable
            );
        }

        public string Summary
        {
            get { return this._summary; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Attempted to set a `string` to null, empty, or just whitespace.");

                this._summary = value;
            }
        }
        private string _summary;

        public PointTotal Points
        {
            get { return this._points; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Points");

                this._points = value;
            }
        }
        private PointTotal _points;

        public Level Level
        {
            get { return this._level; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Level");

                this._checkLevels(value, this._minLevel);
                this._level = value;
            }
        }
        private Level _level;

        public Level MinLevel
        {
            get { return this._minLevel; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("MinLevel");

                this._checkLevels(this._level, value);
                this._minLevel = value;
            }
        }
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

        /// <summary>
        /// This auto-property controls whether or not the Historian ability
        /// can be used on this task.
        /// </summary>
        public bool isHistorianable { get; set; }

        public Name FactionId
        {
            get { return this._factionId; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("FactionId");

                this._factionId = value;
            }
        }
        private Name _factionId;
    }
}