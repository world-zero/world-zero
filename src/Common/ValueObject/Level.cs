using WorldZero.Common.Interface;
using System;
using System.Collections.Generic;

namespace WorldZero.Common.ValueObject
{
    /// <summary>
    /// A Level is a ValueObject that contains a valid level. A level is valid
    /// iff it is not below zero.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// This is thrown on set iff the level is invalid.</exception>
    public class Level : IValueObject
    {
        private int _points;
        public int Get 
        {
            get { return this._points; }
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Level cannot contain a negative value.");
                this._points = value;
            }
        }

        public Level(int value)
        {
            this.Get = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Get;
        }
    }
}