using WorldZero.Domain.Interface;
using System;
using System.Collections.Generic;

namespace WorldZero.Domain.ValueObject
{
    /// <summary>
    /// An Id is a ValueObject that contains a valid ID. An ID is valid iff it
    /// is non-negative.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// This is thrown on ID set iff the ID is invalid.</exception>
    public class Id : IValueObject
    {
        private int _val;
        public int Get
        {
            get { return this._val; }
            private set
            {
                if (value < 0)
                    throw new ArgumentException("An ID cannot be negative.");
                this._val = value;
            }
        }

        public Id(int id)
        {
            this.Get = id;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Get;
        }
    }
}