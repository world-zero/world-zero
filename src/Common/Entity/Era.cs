using WorldZero.Common.Interface.Entity;
using WorldZero.Common.ValueObject.General;
using System;

namespace WorldZero.Common.Entity
{
    /// <summary>
    /// An Era is a configuration file for the game at any given time.
    /// </summary>
    public class Era : INamedEntity
    {
        public Era(
            Name name,
            PastDate startDate,
            Level taskLevelDelta=null,
            int maxPraxises=20,
            int maxTasks=1,
            int maxTasksReiterator=2,
            double penaltyDeduction=0.1,
            bool isFlatPenalty=false,
            PastDate endDate=null
        )
            : base(name)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;

            this.TaskLevelDelta = taskLevelDelta;
            this.MaxPraxises = maxPraxises;
            this.MaxTasksReiterator = maxTasksReiterator;
            this.MaxTasks = maxTasks;
            this.PenaltyDeduction = penaltyDeduction;
            this.IsFlatPenalty = isFlatPenalty;
        }

        internal Era(
            string name,
            DateTime startDate,
            int taskLevelDelta,
            int maxPraxises,
            int maxTasks,
            int maxTasksReiterator,
            double penaltyDeduction,
            bool isFlatPenalty,
            DateTime endDate
        )
            : base(new Name(name))
        {
            this.StartDate = new PastDate(startDate);
            if (endDate != null)
                this.StartDate = new PastDate(endDate);
            this.TaskLevelDelta = new Level(taskLevelDelta);
            this.MaxPraxises = maxPraxises;
            this.MaxTasks = maxTasks;
            this.MaxTasksReiterator = maxTasksReiterator;
            this.PenaltyDeduction = penaltyDeduction;
            this.IsFlatPenalty = isFlatPenalty;
        }

        public override IEntity<Name, string> Clone()
        {
            return new Era(
                this.Id,
                this.StartDate,
                this.TaskLevelDelta,
                this.MaxPraxises,
                this.MaxTasks,
                this.MaxTasksReiterator,
                this.PenaltyDeduction,
                this.IsFlatPenalty,
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

        /// <summary>
        /// This is the number of levels someone can be under for a task to be
        /// allowed to submit a praxis for it.
        /// </summary>
        /// <value>
        /// If null is supplied, this set the field to `new Level(2)`.
        /// </value>
        /// <remarks>
        /// An example of this would be how a character of level 3 would be
        /// able to submit a praxis for a task of level 5 if `LevelDelta` is 2.
        /// </remarks>
        public Level TaskLevelDelta
        {
            get { return this._taskLevelDelta; }
            set
            {
                if (value == null)
                    this._taskLevelDelta = new Level(2);
                else
                    this._taskLevelDelta = value;
            }
        }
        private Level _taskLevelDelta;

        /// <summary>
        /// This is the max number of praxises a character can have in progress
        /// and active, cummulative.
        /// </summary>
        /// <value>
        /// This must be at least 1.
        /// </value>
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

        /// <summary>
        /// This is the max number of times a character can complete any given
        /// task.
        /// </summary>
        /// <value>
        /// This must be at least 1 and no larger than `MaxTasksReiterator`.
        /// </value>
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

        /// <summary>
        /// This is the max number of times a character can complete any given
        /// task iff they have the Reiterator ability.
        /// </summary>
        /// <value>
        /// This must be at least 1 and at least as large as `MaxTasks`.
        /// </value>
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

        /// <summary>
        /// This is the penalty deduction a praxis can receive. This can be
        /// either a flat deduction or a percentage deduction. This logic is
        /// determined by `IsFlatPenalty`.
        /// </summary>
        /// <value>
        /// This is already treated as a deduction, it will not accept negative
        /// values.
        /// </value>
        /// <remarks>
        /// If `IsFlatPenalty` is false, then this will act as the percentage
        /// of the point total to remove.
        /// </remarks>
        public double PenaltyDeduction
        {
            get { return this._penaltyDeduction; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("A penalty should not be negative, it will already be treated as a deduction.");
                this._penaltyDeduction = value;
            }
        }
        private double _penaltyDeduction;

        /// <summary>
        /// A praxis can receive a penalty deduction that can be either a flat-
        /// rate deduction or a percentage deduction - this choice is
        /// controlled with this property, and the value is determined by
        /// `PenaltyDeduction`.
        /// </summary>
        public bool IsFlatPenalty { get; set; }

        /// <summary>
        /// This uses `PenaltyDeduction` and `IsFlatPenalty` to determine the
        /// new point total given the supplied point total when applying the
        /// penalty, rounding down. If the total goes below zero, this will
        /// return `new PointTotal(0)`.
        /// </summary>
        public PointTotal ApplyPenalty(PointTotal pt)
        {
            if (pt == null)
                throw new ArgumentNullException("pt");

            if (this.IsFlatPenalty)
                return this._applyFlatPenalty(pt);
            else
                return this._applyPercentPenalty(pt);
        }

        private PointTotal _applyFlatPenalty(PointTotal pt)
        {
            try
            {
                int r = pt.Get - Convert.ToInt32(this.PenaltyDeduction);
                return new PointTotal(r);
            }
            catch (OverflowException e)
            { throw new ArgumentException("PenaltyDeduction is too large to treat as an int.", e); }
            catch (ArgumentException)
            { return new PointTotal(0); }
        }

        private PointTotal _applyPercentPenalty(PointTotal pt)
        {
            try
            {
                var given     = Convert.ToDouble(pt.Get);
                var deduction = given * this.PenaltyDeduction;
                var result    = given - deduction;
                return new PointTotal(Convert.ToInt32(result));
            }
            catch (OverflowException e)
            { throw new ArgumentException("The new result could not be converted to an int.", e); }
            catch (InvalidCastException e)
            { throw new ArgumentException("The new result could not be converted to an int.", e); }
            catch (ArgumentException)
            { return new PointTotal(0); }
        }

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