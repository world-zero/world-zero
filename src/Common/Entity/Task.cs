using System;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.ValueObject;

namespace WorldZero.Common.Entity
{
    /// <summary>
    /// Task is a entity for a tuple of the Task table.
    /// </summary>
    public class Task : IIdEntity
    {
        public Task(
            Name factionId,
            Name statusId,
            string summary,
            PointTotal points,
            Level level,
            Level minLevel=null
        )
            : base()
        {
            this._setup(
                summary,
                level,
                minLevel,
                points,
                factionId,
                statusId
            );
        }

        public Task(
            Id id,
            Name factionId,
            Name statusId,
            string summary,
            PointTotal points,
            Level level,
            Level minLevel=null
        )
            : base(id)
        {
            this._setup(
                summary,
                level,
                minLevel,
                points,
                factionId,
                statusId
            );
        }

        internal Task(
            int id,
            string factionId,
            string statusId,
            string summary,
            int points,
            int level,
            int minLevel
        )
            : base(new Id(id))
        {
            this._setup(
                summary,
                new Level(level),
                new Level(minLevel),
                new PointTotal(points),
                new Name(factionId),
                new Name(statusId)
            );
        }

        private void _setup(string summary, Level level, Level minLevel, PointTotal points, Name factionId, Name statusId)
        {
            this.Summary = summary;
            this.Level = level;
            if (minLevel == null) this.MinLevel = new Level(0);
            else                  this.MinLevel = minLevel;
            this.Points = points;
            this.FactionId = factionId;
            this.StatusId = statusId;
        }

        public override IEntity<Id, int> DeepCopy()
        {
            return new Task(
                this.Id,
                this.FactionId,
                this.StatusId,
                this.Summary,
                this.Points,
                this.Level,
                this.MinLevel
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
                    throw new ArgumentException("Attempted to set a `PointTotal` to null.");

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
                    throw new ArgumentException("Attempted to set a `Level` to null.");

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
                    throw new ArgumentException("Attempted to set a `Level` to null.");

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

        public Name FactionId
        {
            get { return this._factionId; }
            set
            {
                if (value == null)
                    throw new ArgumentException("Attempted to set an `Id` to null.");

                this._factionId = value;
            }
        }
        private Name _factionId;

        public Name StatusId
        {
            get { return this._statusId; }
            set
            {
                if (value == null)
                    throw new ArgumentException("Attempted to set an `Id` to null.");

                this._statusId = value;
            }
        }
        private Name _statusId;
    }
}