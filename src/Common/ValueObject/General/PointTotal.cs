using WorldZero.Common.Interface.General.Generic;
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
    public sealed class PointTotal : ISingleValueObject<Double>
    {
        public override double Get 
        {
            get { return this._val; }
            protected set
            {
                if (value < 0)
                    throw new ArgumentException("PointTotal cannot contain a negative value.");
                this._val = value;
            }
        }

        /// <summary>
        /// Return the stored value as an int, rounding down.
        /// </summary>
        /// <remarks>
        /// Be warned that doubles can store larger values than 32-bit ints.
        /// This property will do nothing to catch the exceptions caused by
        /// `Convert.ToInt32(double)`.
        /// </remarks>
        public int AsInt { get { return Convert.ToInt32(this._val); } }

        public PointTotal(double value)
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
            PointTotal penalty,
            bool isFlatPenalty=true
        )
        {
            if (pt == null)
                throw new ArgumentNullException("pt");
            if (penalty == null)
                throw new ArgumentNullException("penalty");

            if (isFlatPenalty)
                return _applyFlatPenalty(pt, penalty, isFlatPenalty);
            else
                return _applyPercentPenalty(pt, penalty, isFlatPenalty);
        }

        private static PointTotal _applyFlatPenalty(
            PointTotal pt,
            PointTotal penalty,
            bool isFlatPenalty
        )
        {
            try
            {
                double r = pt.Get - penalty.Get;
                return new PointTotal(r);
            }
            catch (ArgumentException)
            { return new PointTotal(0); }
        }

        private static PointTotal _applyPercentPenalty(
            PointTotal pt,
            PointTotal penalty,
            bool isFlatPenalty
        )
        {
            try
            {
                var given     = pt.Get;
                var deduction = pt.Get * penalty.Get;
                var result    = given - deduction;
                return new PointTotal(result);
            }
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
            PointTotal bonus,
            bool isFlatBonus=true
        )
        {
            if (pt == null)
                throw new ArgumentNullException("pt");
            if (bonus == null)
                throw new ArgumentNullException("bonus");

            if (isFlatBonus)
                return _applyFlatBonus(pt, bonus, isFlatBonus);
            else
                return _applyPercentBonus(pt, bonus, isFlatBonus);
        }

        private static PointTotal _applyFlatBonus(
            PointTotal pt,
            PointTotal bonus,
            bool isFlatBonus
        )
        {
            try
            {
                double r = pt.Get + bonus.Get;
                return new PointTotal(r);
            }
            catch (ArgumentException e)
            { throw new InvalidOperationException("This shouldn't be happening.", e); }
        }

        private static PointTotal _applyPercentBonus(
            PointTotal pt,
            PointTotal bonus,
            bool isFlatBonus
        )
        {
            try
            {
                var given  = Convert.ToDouble(pt.Get);
                var yay    = given * bonus.Get;
                var result = given + yay;
                return new PointTotal(result);
            }
            catch (ArgumentException e)
            { throw new InvalidOperationException("This shouldn't be happening.", e); }
        }
    }
}