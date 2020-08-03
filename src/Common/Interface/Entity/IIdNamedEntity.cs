using System.ComponentModel.DataAnnotations.Schema;
using WorldZero.Common.ValueObject;
using System.ComponentModel.DataAnnotations;

namespace WorldZero.Common.Interface.Entity
{
    /// <inheritdoc cref="IIdEntity">
    /// <summary>
    /// This class exists to have entities that have an `Id` ID property, and
    /// also have a name property - critically, these name must be unique. As
    /// with this type of rule, it should be enforced by the repo.
    /// </summary>
    /// <remarks>
    /// This is necessary instead of an `INamedEntity` as a primary key
    /// generally cannot be changed.
    /// </remarks>
    public abstract class IIdNamedEntity : IIdEntity
    {
        [Required]
        [MaxLength(ValueObject.Name.MaxLength)]
        /// <summary>
        /// This is a `Name` that is not an ID of an entity, but rather is a
        /// `Name` that a repo must enforce to be unique.
        /// </summary>
        public string Name
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._name,
                    null);
            }
            set { this._name = new Name(value); }
        }
        [NotMapped]
        private Name _name;
    }
}
