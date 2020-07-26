using WorldZero.Common.ValueObject;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Interface
{
    /// <inheritdoc cref="IEntity">
    /// <summary>
    /// This class is used for entities that have a `Id` primary key.
    /// </summary>
    public abstract class IIdEntity : IEntity<int>
    {
        // TODO: will the annodations stack like they do normally?
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
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