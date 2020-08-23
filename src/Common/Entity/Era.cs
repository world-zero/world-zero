using WorldZero.Common.Interface.Entity;
using WorldZero.Common.ValueObject;
using System;

namespace WorldZero.Common.Entity
{
    /// <summary>
    /// Era is a entity for a tuple of the Era table.
    /// </summary>
    public class Era : INamedEntity
    {
        public Era(Name name, PastDate startDate, PastDate endDate=null)
            : base(name)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        internal Era(string name, DateTime startDate, DateTime endDate)
            : base(new Name(name))
        {
            this.StartDate = new PastDate(startDate);
            if (endDate != null)
                this.StartDate = new PastDate(endDate);
        }

        public override IEntity<Name, string> DeepCopy()
        {
            return new Era(
                this.Id,
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
                    throw new ArgumentException("Attempted to set a `PastDate` to null.");

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