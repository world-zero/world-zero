using WorldZero.Common.Interface;
using System;

namespace WorldZero.Common.ValueObject
{
    /// <summary>
    /// A Name is a ValueObject that contains a valid name. A name is valid iff
    /// it is not null, not empty, not just whitespace, and if it is greater
    /// than 25 characters.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// This is thrown on name set iff the name is invalid.</exception>
    /// <remarks>
    /// The reason a name cannot be longer than 25 characters because EF Core
    /// is having a fit about it.
    /// </remarks>
    public class Name : ISingleValueObject<string>
    {
        public const int MaxLength = 25;

        public override string Get
        {
            get { return this._val; }
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("A name cannot be null, empty, or just whitespace.");
                if (value.Length > MaxLength)
                    throw new ArgumentException("A name cannot be longer than 25 characters.");
                this._val = value;
            }
        }

        public Name(string name)
            : base(name)
        {
        }
    }
}