using WorldZero.Common.Interface;
using System;
using System.Collections.Generic;

namespace WorldZero.Common.ValueObject
{
    /// <summary>
    /// A PointTotal is a ValueObject that contains a valid point total. A
    /// point total is valid iff it is not below zero.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// This is thrown on set iff the point total is invalid.</exception>
    public class PointTotal : IValueObject
    {
        private int _points;
        public int Get 
        {
            get { return this._points; }
            private set
            {
                if (value < 0)
                    throw new ArgumentException("PointTotal cannot contain a negative value.");
                this._points = value;
            }
        }

        public PointTotal(int value)
        {
            this.Get = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Get;
        }
    }
}