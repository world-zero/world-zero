using WorldZero.Common.Interface;
using System;

namespace WorldZero.Common.ValueObject.General
{
    /// <summary>
    /// A PointTotal is a ValueObject that contains a valid point total. A
    /// point total is valid iff it is not below zero.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// This is thrown on set iff the point total is invalid.
    /// </exception>
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

        /// <summary>
        /// This uses `penalty` and `isFlatPenalty` to determine the
        /// new point total given the supplied point total when applying the
        /// penalty, rounding down. If the total goes below zero, this will
        /// return `new PointTotal(0)`.
        /// </summary>
        /// </summary>
        public static PointTotal ApplyPenalty(
            PointTotal pt,
            double penalty,
            bool isFlatPenalty=true
        )
        {
            if (pt == null)
                throw new ArgumentNullException("pt");

            penalty = Math.Abs(penalty);
            if (isFlatPenalty)
                return _applyFlatPenalty(pt, penalty, isFlatPenalty);
            else
                return _applyPercentPenalty(pt, penalty, isFlatPenalty);
        }

        private static PointTotal _applyFlatPenalty(
            PointTotal pt,
            double penalty,
            bool isFlatPenalty
        )
        {
            try
            {
                int r = pt.Get - Convert.ToInt32(penalty);
                return new PointTotal(r);
            }
            catch (OverflowException e)
            { throw new ArgumentException("PenaltyDeduction is too large to treat as an int.", e); }
            catch (ArgumentException)
            { return new PointTotal(0); }
        }

        private static PointTotal _applyPercentPenalty(
            PointTotal pt,
            double penalty,
            bool isFlatPenalty
        )
        {
            try
            {
                var given     = Convert.ToDouble(pt.Get);
                var deduction = given * penalty;
                var result    = given - deduction;
                return new PointTotal(Convert.ToInt32(result));
            }
            catch (OverflowException e)
            { throw new ArgumentException("The new result could not be converted to an int.", e); }
            catch (InvalidCastException e)
            { throw new ArgumentException("The new result could not be converted to an int.", e); }
            catch (ArgumentException)
            { return new PointTotal(0); }
        }

        /// <summary>
        /// This uses `bonus` and `isFlatBonus` to determine the
        /// new point total given the supplied point total when applying the
        /// bonus, rounding down. If the total goes below zero, this will
        /// return `new PointTotal(0)`.
        /// </summary>
        public static PointTotal ApplyBonus(
            PointTotal pt,
            double bonus,
            bool isFlatBonus=true
        )
        {
            if (pt == null)
                throw new ArgumentNullException("pt");

            bonus = Math.Abs(bonus);
            if (isFlatBonus)
                return _applyFlatBonus(pt, bonus, isFlatBonus);
            else
                return _applyPercentBonus(pt, bonus, isFlatBonus);
        }

        private static PointTotal _applyFlatBonus(
            PointTotal pt,
            double bonus,
            bool isFlatBonus
        )
        {
            try
            {
                int r = pt.Get + Convert.ToInt32(bonus);
                return new PointTotal(r);
            }
            catch (OverflowException e)
            { throw new ArgumentException("BonusDeduction is too large to treat as an int.", e); }
            catch (ArgumentException e)
            { throw new InvalidOperationException("This shouldn't be happening.", e); }
        }

        private static PointTotal _applyPercentBonus(
            PointTotal pt,
            double bonus,
            bool isFlatBonus
        )
        {
            try
            {
                var given  = Convert.ToDouble(pt.Get);
                var yay    = given * bonus;
                var result = given + yay;
                return new PointTotal(Convert.ToInt32(result));
            }
            catch (OverflowException e)
            { throw new ArgumentException("The new result could not be converted to an int.", e); }
            catch (InvalidCastException e)
            { throw new ArgumentException("The new result could not be converted to an int.", e); }
            catch (ArgumentException e)
            { throw new InvalidOperationException("This shouldn't be happening.", e); }
        }
    }
}