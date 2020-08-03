using WorldZero.Common.ValueObject;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Interface.Entity
{
    /// <inheritdoc cref="IEntity">
    /// <summary>
    /// This class is used for entities that have a `Id` primary key.
    /// </summary>
    public abstract class IIdEntity : IEntity<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// The ID of this entity is an `Id`, and it is accessed via this
        /// property.
        /// </summary>
        public override int Id
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._id,
                    0);
            }
            set { this._id = new Id(value); }
        }
    }
}