using System;
using System.Collections.Generic;
using WorldZero.Common.Interface.ValueObject;

namespace WorldZero.Common.ValueObject.DTO.General.Generic
{
    /// <summary>
    /// This is a DTO that correlates a countee to an int count.
    /// </summary>
    /// <typeparam name="TCountee">
    /// The type of the thing being counted - no restrictions apply.
    /// </typeparam>
    /// <remarks>
    /// This was created to allow for easy-to-use return values from certain
    /// repository methods, specifically to make it easy to play with Dapper.
    /// </remarks>
    public class CountingDTO<TCountee> : IValueObject
    {
        public readonly TCountee Countee;
        public readonly int Count;

        /// <summary>
        /// Construct a new instance.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown iff countee can be null and is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown iff count is negative.
        /// </exception>
        public CountingDTO(TCountee countee, int count)
        {
            if (countee == null)
                throw new ArgumentNullException("countee");
            if (count < 0)
                throw new ArgumentException("count cannot be negative.");
            this.Countee = countee;
            this.Count = count;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Countee;
            yield return this.Count;
        }
    }
}