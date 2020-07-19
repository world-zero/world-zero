using WorldZero.Common.ValueObject;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Interface
{
    /// <summary>
    /// This class is an entity with a name as well as an ID. This name is a
    /// wrapper around a Name value object.
    /// </summary>
    public abstract class INamedEntity : IEntity
    {
        [Required, MaxLength(ValueObject.Name.MaxLength)]
        /// <summary>
        /// Name is a wrapper for a <c>Name</c> - no exceptions are
        /// caught. Enforcing the uniqueness of a name is left to the repo.
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