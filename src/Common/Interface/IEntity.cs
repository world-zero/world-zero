using WorldZero.Common.ValueObject;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("WorldZero.Data")]
[assembly: InternalsVisibleTo("Test.Unit")]
[assembly: InternalsVisibleTo("WorldZero.Test.Integration")]

namespace WorldZero.Common.Interface
{
    /// <summary>
    /// IEntity is the parent that the different entity implementations. This
    /// does no special work to compare two entities.
    /// </summary>
    /// <remarks>
    /// Again, because of not wanting to fight EF Core even more than I already
    /// have, this will use a value object ID field with a primative property.
    /// </remarks>
    public abstract class IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// Id is a wrapper for an <c>Id</c> - no exceptions are caught.
        /// Enforcing the uniqueness of an ID of a particular entity is left to
        /// the repository of that entity.
        /// </summary>
        public int Id
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._id,
                    0);
            }
            set { this._id = new Id(value); }
        }
        [NotMapped]
        private Id _id;

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
        protected T Eval<T>(ISingleValueObject<T> svo, T other)
        {
            if (svo != null)
                return svo.Get;
            else
                return other;
        }
    }
}