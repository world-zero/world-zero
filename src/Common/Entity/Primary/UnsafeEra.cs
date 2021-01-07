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
            Level taskLevelDelta=null,
            int maxPraxises=20,
            int maxTaskCompletion=1,
            int maxTaskCompletionReiterator=2,
            PastDate startDate=null,
            PastDate endDate=null
        )
            : base(name)
        {
            if (startDate == null)
                startDate = new PastDate(DateTime.UtcNow);
            this.StartDate = startDate;
            this.EndDate = endDate;

            this.TaskLevelBuffer = taskLevelDelta;
            this.MaxPraxises = maxPraxises;
            this.MaxTaskCompletionReiterator = maxTaskCompletionReiterator;
            this.MaxTaskCompletion = maxTaskCompletion;
        }

        internal UnsafeEra(
            string name,
            int taskLevelDelta,
            int maxPraxises,
            int maxTaskCompletion,
            int maxTaskCompletionReiterator,
            DateTime startDate,
            DateTime endDate
        )
            : base(new Name(name))
        {
            this.StartDate = new PastDate(startDate);
            this.EndDate = new PastDate(endDate);
            this.TaskLevelBuffer = new Level(taskLevelDelta);
            this.MaxPraxises = maxPraxises;
            this.MaxTaskCompletion = maxTaskCompletion;
            this.MaxTaskCompletionReiterator = maxTaskCompletionReiterator;
        }

        public override IEntity<Name, string> CloneAsEntity()
        {
            return new UnsafeEra(
                this.Id,
                this.TaskLevelBuffer,
                this.MaxPraxises,
                this.MaxTaskCompletion,
                this.MaxTaskCompletionReiterator,
                this.StartDate,
                this.EndDate
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