using WorldZero.Common.Interface;
using System;

namespace WorldZero.Common.ValueObject
{
    /// <summary>
    /// A PastDate is a ValueObject that contains a valid date. A date is valid iff
    /// it is not in the future.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// This is thrown on date set iff the date is invalid.</exception>
    public class PastDate : ISingleValueObject<DateTime>
    {
        public override DateTime Get
        {
            get { return this._val; }
            protected set
            {
                if (DateTime.UtcNow < value)
                    throw new ArgumentException("A PastDate cannot have a DateTime that is in the future.");
                this._val = value;
            }
        }

        public PastDate(DateTime date)
            : base(date)
        {
        }
    }
}