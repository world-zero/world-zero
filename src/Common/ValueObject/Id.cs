using WorldZero.Common.Interface;
using System;

namespace WorldZero.Common.ValueObject
{
    /// <summary>
    /// An Id is a ValueObject that contains a valid ID. An ID is valid iff it
    /// is non-negative.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// This is thrown on ID set iff the ID is invalid.</exception>
    /// <remarks>
    /// The cutoff at 0 instead of 1 is because the entities will default to not
    /// defining an ID, and EF Core starts counting IDs at 1, meaning that an
    /// ID that is 0 is unregistered with a repo, an ID greater than 0 is
    /// registered, and an ID less than 0 is invalid.
    /// </remarks>
    public class Id : ISingleValueObject<int>
    {
        public override int Get
        {
            get { return this._val; }
            protected set
            {
                if (value < 0)
                    throw new ArgumentException("An ID cannot be negative.");
                this._val = value;
            }
        }

        public Id(int id)
            : base(id)
        {
        }
    }
}