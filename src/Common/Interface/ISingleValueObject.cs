using System.Collections.Generic;

namespace WorldZero.Common.Interface
{
    /// <summary>
    /// This class will serve as a contract for single-value ValueObject.
    /// </summary>
    /// <remarks>
    /// In truth, this really isn't very necessary, except EF Core is a little
    /// stinker and I do not have the energy to bludgeon it into accepting
    /// value objects, so here we are.
    /// </remarks>
    /// <typeparam name="T">The type of the single value.</typeparam>
    public abstract class ISingleValueObject<T> : IValueObject
    {
        protected T _val;
        public abstract T Get { get; protected set; }

        public ISingleValueObject(T value)
        {
            this.Get = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Get;
        }
    }
}