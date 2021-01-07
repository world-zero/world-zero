using System;
using WorldZero.Common.Interface.DTO;

namespace WorldZero.Common.DTO.General.Generic
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
    public class CountingDTO<TCountee> : IDTO
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

        public object Clone()
        {
            return new CountingDTO<TCountee>(this.Countee, this.Count);
        }

        public override bool Equals(object o)
        {
            return this.Equals(o as IDTO);
        }

        public bool Equals(IDTO dto)
        {
            CountingDTO<TCountee> other = dto as CountingDTO<TCountee>;
            if (other == null)                       return false;
            if (this.Count != other.Count)           return false;
            if (!this.Countee.Equals(other.Countee)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int counteeHash = this.Countee.GetHashCode();
                return counteeHash
                       * 7
                       + (counteeHash * this.Count+1);
            }
        }
    }
}