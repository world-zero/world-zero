using System.Collections.Generic;

namespace WorldZero.Common.Interface.ValueObject
{
    /// <summary>
    /// This class will serve as a contract for single-value ValueObject. These
    /// children may have other members, but they are really just auxilory to
    /// the member defined here (such as a max length member).
    /// </summary>
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