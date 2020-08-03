using WorldZero.Common.Interface;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Entity
{
    [Table("Location")]
    /// <summary>
    /// Location is a entity for a tuple of the Location table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class Location : IIdEntity
    {
        [Required]
        /// <summary>
        /// City is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
        public string City
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._city,
                    null);
            }
            set { this._city = new Name(value); }
        }
        [NotMapped]
        private Name _city;

        [Required]
        /// <summary>
        /// State is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
        public string State
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._state,
                    null);
            }
            set { this._state = new Name(value); }
        }
        [NotMapped]
        private Name _state;

        [Required]
        /// <summary>
        /// Country is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
        public string Country
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._country,
                    null);
            }
            set { this._country = new Name(value); }
        }
        [NotMapped]
        private Name _country;

        [Required]
        /// <summary>
        /// Zip is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
        public string Zip
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._zip,
                    null);
            }
            set { this._zip = new Name(value); }
        }
        [NotMapped]
        private Name _zip;

        internal virtual ICollection<Character> Characters { get; set; }
    }
}