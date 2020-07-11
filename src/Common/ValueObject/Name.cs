using WorldZero.Common.Interface;
using System;
using System.Collections.Generic;

namespace WorldZero.Common.ValueObject
{
    /// <summary>
    /// A Name is a ValueObject that contains a valid name. A name is valid iff
    /// it is not null, not empty, and not just whitespace.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// This is thrown on name set iff the name is invalid.</exception>
    public class Name : IValueObject
    {
        private string _val;
        public string Get
        {
            get { return this._val; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("A name cannot be null, empty, or just whitespace.");
                this._val = value;
            }
        }

        public Name(string name)
        {
            this.Get = name;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Get;
        }
    }
}