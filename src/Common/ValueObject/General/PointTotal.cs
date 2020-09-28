using WorldZero.Common.Interface;
using System;

namespace WorldZero.Common.ValueObject.General
{
    /// <summary>
    /// A PointTotal is a ValueObject that contains a valid point total. A
    /// point total is valid iff it is not below zero.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// This is thrown on set iff the point total is invalid.</exception>
    public sealed class PointTotal : ISingleValueObject<int>
    {
        public override int Get 
        {
            get { return this._val; }
            protected set
            {
                if (value < 0)
                    throw new ArgumentException("PointTotal cannot contain a negative value.");
                this._val = value;
            }
        }

        public PointTotal(int value)
            : base(value)
        { }
    }
}