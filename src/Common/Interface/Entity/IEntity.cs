using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("WorldZero.Data")]
[assembly: InternalsVisibleTo("Test.Unit")]
[assembly: InternalsVisibleTo("WorldZero.Test.Integration")]

namespace WorldZero.Common.Interface.Entity
{
    /// <summary>
    /// This is the interface for an Entity with an Id.
    /// </summary>
    /// <remarks>
    /// Again, because of not wanting to fight EF Core even more than I already
    /// have, this will use a value object ID field with a primative property.
    /// </remarks>
    public abstract class IEntity<T>
    {
        [Key]
        /// <summary>
        /// This is the abstract property for the underlying
        /// `ISingleValueObject` that children must implement.
        /// </summary>
        /// <value>
        /// The built-in type of the underlying ISingleValueObject.
        /// </value>
        public abstract T Id { get; set; }
        [NotMapped]
        protected ISingleValueObject<T> _id;


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
        /// <example>
        /// Here is a snippit of the tests of this method.
        /// <code>
        /// var name = new Name("Pizza");
        /// string expected = name.Get;
        /// string result = this._e.Eval<string>((ISingleValueObject<string>) name, "Pie");
        /// Assert.AreEqual(expected, result);
        /// result = this._e.Eval<string>((ISingleValueObject<string>) null, "Pie");
        /// Assert.AreEqual("Pie", result);
        /// </code>
        /// </example>
        protected T0 Eval<T0>(ISingleValueObject<T0> svo, T0 other)
        {
            if (svo != null)
                return svo.Get;
            else
                return other;
        }
    }
}