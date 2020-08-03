using WorldZero.Common.ValueObject;
using System.ComponentModel.DataAnnotations;

namespace WorldZero.Common.Interface.Entity
{
    /// <inheritdoc cref="IEntity">
    /// <summary>
    /// This class is used for entities that have a `Name` primary key.
    /// </summary>
    public abstract class INamedEntity : IEntity<string>
    {
        [MaxLength(ValueObject.Name.MaxLength)]
        /// <summary>
        /// The ID of this entity is a `Name`, and it is accessed via this
        /// property.
        /// </summary>
        public override string Id
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._id,
                    null);
            }
            set { this._id = new Name(value); }
        }
    }
}