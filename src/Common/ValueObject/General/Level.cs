using WorldZero.Common.Interface;
using System;

namespace WorldZero.Common.ValueObject.General
{
    /// <summary>
    /// A Level is a ValueObject that contains a valid level. A level is valid
    /// iff it is not below zero.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// This is thrown on set iff the level is invalid.</exception>
    public class Level : ISingleValueObject<int>
    {
        public override int Get 
        {
            get { return this._val; }
            protected set
            {
                if (value < 0)
                    throw new ArgumentException("Level cannot contain a negative value.");
                this._val = value;
            }
        }

        public Level(int value)
            : base(value)
        {
        }
    }
}