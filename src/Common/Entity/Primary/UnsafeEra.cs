using WorldZero.Common.DTO.Entity.Primary;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.ValueObject.General;
using System;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="IEra"/>
    public class UnsafeEra : ABCNamedEntity, IEra
    {
        public UnsafeEra(
            Name name,
            Level taskLevelBuffer=null,
            int maxPraxises=20,
            int maxTaskCompletion=1,
            int maxTaskCompletionReiterator=2,
            PastDate startDate=null,
            PastDate endDate=null
        )
            : base(name)
        {
            this._setup(
                taskLevelBuffer,
                maxPraxises,
                maxTaskCompletion,
                maxTaskCompletionReiterator,
                startDate,
                endDate
            );
        }

        public UnsafeEra(IEraDTO dto)
            : base(dto.Id)
        {
            this._setup(
                dto.TaskLevelBuffer,
                dto.MaxPraxises,
                dto.MaxTaskCompletion,
                dto.MaxTaskCompletionReiterator,
                dto.StartDate,
                dto.EndDate
            );
        }

        private void _setup(
            Level taskLevelBuffer,
            int maxPraxises,
            int maxTaskCompletion,
            int maxTaskCompletionReiterator,
            PastDate startDate,
            PastDate endDate
        )
        {
            if (startDate == null)
                startDate = new PastDate(DateTime.UtcNow);
            this.StartDate = startDate;
            this.EndDate = endDate;

            this.TaskLevelBuffer = taskLevelBuffer;
            this.MaxPraxises = maxPraxises;
            this.MaxTaskCompletionReiterator = maxTaskCompletionReiterator;
            this.MaxTaskCompletion = maxTaskCompletion;
        }

        public override object Clone()
        {
            return new EraDTO(
                this.Id,
                this.StartDate,
                this.EndDate,
                this.TaskLevelBuffer,
                this.MaxPraxises,
                this.MaxTaskCompletion,
                this.MaxTaskCompletionReiterator
            );
        }

        public PastDate StartDate
        {
            get { return this._startDate; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("StartDate");

                this._checkDates(value, this.EndDate);
                this._startDate = value;
            }
        }
        private PastDate _startDate;

        public PastDate EndDate
        {
            get { return this._endDate; }
            set
            {
                this._checkDates(this.StartDate, value);
                this._endDate = value;
            }
        }
        private PastDate _endDate;

        /// <value>
        /// If null is supplied, this set the field to `new Level(2)`.
        /// </value>
        public Level TaskLevelBuffer
        {
            get { return this._taskLevelBuffer; }
            set
            {
                if (value == null)
                    this._taskLevelBuffer = new Level(2);
                else
                    this._taskLevelBuffer = value;
            }
        }
        private Level _taskLevelBuffer;

        public int MaxPraxises
        {
            get { return this._maxPraxises; }
            set
            {
                if (value < 1)
                    throw new ArgumentException("People must be able to register for at least one task at a time.");
                this._maxPraxises = value;
            }
        }
        private int _maxPraxises;

        public int MaxTaskCompletion
        {
            get { return this._maxTaskCompletion; }
            set
            {
                if (value < 1)
                    throw new ArgumentException("People must be able to submit tasks.");
                if (value > this.MaxTaskCompletionReiterator)
                    throw new ArgumentException("MaxTaskCompletion cannot be larger than MaxTaskCompletionReiterator.");
                this._maxTaskCompletion = value;
            }
        }
        private int _maxTaskCompletion = 1;

        public int MaxTaskCompletionReiterator
        {
            get { return this._maxTaskCompletionReiterator; }
            set
            {
                if (value < 1)
                    throw new ArgumentException("People must be able to submit tasks.");
                if (value < this.MaxTaskCompletion)
                    throw new ArgumentException("MaxTaskCompletionReiterator cannot be smaller than MaxTaskCompletion.");
                this._maxTaskCompletionReiterator = value;
            }
        }
        private int _maxTaskCompletionReiterator = 1;

        private void _checkDates(PastDate start, PastDate end)
        {
            if ( (start != null) 
                && (end != null)
                && (end.Get < start.Get) )
            {
                throw new ArgumentException("The end date must be after the start date.");
            }
        }
    }
}