namespace WorldZero.Common.Interface
{
    public abstract class IModel
    {
        /// <summary>
        /// This method will concisely return the value stored in the passed
        /// implementation of ISingleValueObject, or, if that argument is null,
        /// it will return the alternate value.
        /// </summary>
        /// <param name="svo">The ISingleValueObject implementation.</param>
        /// <param name="other">The default return value.</param>
        /// <typeparam name="T">
        /// The type of the single value in ISingleValueObject and other.
        /// </typeparam>
        /// <returns>Either the value in svo, if the instance in non-null, or
        /// other.</returns>
        protected T Eval<T>(ISingleValueObject<T> svo, T other)
        {
            if (svo != null)
                return svo.Get;
            else
                return other;
        }
    }
}