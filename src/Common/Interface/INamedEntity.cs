using WorldZero.Common.ValueObject;
using System.ComponentModel.DataAnnotations;

namespace WorldZero.Common.Interface
{
    /// <inheritdoc cref="IEntity">
    /// <summary>
    /// This class is used for entities that have a `Name` primary key.
    /// </summary>
    public abstract class INamedEntity : IEntity<string>
    {
        // TODO: will the annodations stack like they do normally?
        [MaxLength(ValueObject.Name.MaxLength)]
        /// <summary>
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