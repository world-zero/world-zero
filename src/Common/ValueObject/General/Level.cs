using WorldZero.Common.Interface.ValueObject;
using System;

namespace WorldZero.Common.ValueObject.General
{
    /// <summary>
    /// A Level is a ValueObject that contains a valid level. A level is valid
    /// iff it is not below zero.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// This is thrown on set iff the level is invalid.</exception>
    public class Level : ABCSingleValueObject<int>
    {
        /// <summary>
        /// Determine the level based off the number of points supplied.
        /// </summary>
        /// <param name="points">The points to calculate the level of.</param>
        /// <returns><c>Level</c> corresponding to the <c>points</c>.</returns>
        public static Level CalculateLevel(PointTotal points)
        {
            if (points == null)
                throw new ArgumentNullException("points");

            int r = -1; // Just to make sure it's getting set.
            int p = points.AsInt;
            if      (p < 10)   r = 0;
            else if (p < 70)   r = 1;
            else if (p < 170)  r = 2;
            else if (p < 330)  r = 3;
            else if (p < 610)  r = 4;
            else if (p < 1090) r = 5;
            else if (p < 1840) r = 6;
            else if (p < 3040) r = 7;
            else               r = 8;
            try
            {
                return new Level(r);
            }
            catch (ArgumentException e)
            { throw new InvalidOperationException("This should not occur.", e); }
        }

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
        { }
    }
}