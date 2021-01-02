using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Generic.Primary;
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
            int maxTasks=1,
            int maxTasksReiterator=2,
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
            this.MaxTasksReiterator = maxTasksReiterator;
            this.MaxTasks = maxTasks;
        }

        internal UnsafeEra(
            string name,
            int taskLevelDelta,
            int maxPraxises,
            int maxTasks,
            int maxTasksReiterator,
            DateTime startDate,
            DateTime endDate
        )
            : base(new Name(name))
        {
            this.StartDate = new PastDate(startDate);
            this.EndDate = new PastDate(endDate);
            this.TaskLevelBuffer = new Level(taskLevelDelta);
            this.MaxPraxises = maxPraxises;
            this.MaxTasks = maxTasks;
            this.MaxTasksReiterator = maxTasksReiterator;
        }

        public override IEntity<Name, string> CloneAsEntity()
        {
            return new UnsafeEra(
                this.Id,
                this.TaskLevelBuffer,
                this.MaxPraxises,
                this.MaxTasks,
                this.MaxTasksReiterator,
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

        public int MaxTasks
        {
            get { return this._maxTasks; }
            set
            {
                if (value < 1)
                    throw new ArgumentException("People must be able to submit tasks.");
                if (value > this.MaxTasksReiterator)
                    throw new ArgumentException("MaxTasks cannot be larger than MaxTasksReiterator.");
                this._maxTasks = value;
            }
        }
        private int _maxTasks = 1;

        public int MaxTasksReiterator
        {
            get { return this._maxTasksReiterator; }
            set
            {
                if (value < 1)
                    throw new ArgumentException("People must be able to submit tasks.");
                if (value < this.MaxTasks)
                    throw new ArgumentException("MaxTasksReiterator cannot be smaller than MaxTasks.");
                this._maxTasksReiterator = value;
            }
        }
        private int _maxTasksReiterator = 1;

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