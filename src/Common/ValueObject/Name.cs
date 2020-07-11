using WorldZero.Common.Interface;
using System;

namespace WorldZero.Common.ValueObject
{
    /// <summary>
    /// A Name is a ValueObject that contains a valid name. A name is valid iff
    /// it is not null, not empty, and not just whitespace.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// This is thrown on name set iff the name is invalid.</exception>
    public class Name : ISingleValueObject<string>
    {
        public override string Get
        {
            get { return this._val; }
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("A name cannot be null, empty, or just whitespace.");
                this._val = value;
            }
        }

        public Name(string name)
            : base(name)
        {
        }
    }
}