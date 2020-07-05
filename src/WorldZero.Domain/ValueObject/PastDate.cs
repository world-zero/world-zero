using WorldZero.Domain.Interface;
using System;
using System.Collections.Generic;

namespace WorldZero.Domain.ValueObject
{
    /// <summary>
    /// A PastDate is a ValueObject that contains a valid date. A date is valid iff
    /// it is not in the future.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// This is thrown on name set iff the name is invalid.</exception>
    public class PastDate : IValueObject
    {
        private DateTime _val;
        public DateTime Get
        {
            get { return this._val; }
            private set
            {
                if (DateTime.UtcNow < value)
                    throw new ArgumentException("A PastDate cannot have a datetime that is in the future.");
                this._val = value;
            }
        }

        public PastDate(DateTime date)
        {
            this.Get = date;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Get;
        }
    }
}