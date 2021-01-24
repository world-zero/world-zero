using System;
using WorldZero.Common.DTO.Entity.Primary;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="ITask"/>
    public class UnsafeTask : ABCIdStatusedEntity, ITask
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

        public UnsafeTask(ITaskDTO dto)
            : base(dto.Id, dto.StatusId)
        {
            this._setup(
                dto.Summary,
                dto.Level,
                dto.MinLevel,
                dto.Points,
                dto.FactionId,
                dto.IsHistorianable
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
            this.IsHistorianable = isHistorianable;
            this.FactionId = factionId;
        }

        public override object Clone()
        {
            return new TaskDTO(
                this.Id,
                this.StatusId,
                this.Summary,
                this.FactionId,
                this.Points,
                this.Level,
                this.MinLevel,
                this.IsHistorianable
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

        public bool IsHistorianable { get; set; }

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