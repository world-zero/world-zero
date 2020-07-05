using System.Collections.Generic;
using System;
using WorldZero.Domain.Interface;

namespace WorldZero.Domain.ValueObject
{
    /// <summary>
    /// A PastDate is a GetObject that contains a valid date. A date is valid iff
    /// it is not in the future.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// This is thrown on name set iff the name is invalid.</exception>
    public class Points : IValueObject
    {
        private int _points;
        public int Get 
        {
            get { return this._points; }
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Points cannot contain a negative value.");
                this._points = value;
            }
        }

        public Points(int value)
        {
            this.Get = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Get;
        }
    }
}