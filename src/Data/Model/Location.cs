using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Location")]
    /// <summary>
    /// Location is a model for a tuple of the Location table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class Location : IModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// LocationId is a wrapper for an <c>Id</c> - no exceptions are
        /// caught.
        /// </summary>
        public int LocationId
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._locationId,
                    0);
            }
            set { this._locationId = new Id(value); }
        }
        [NotMapped]
        private Id _locationId;

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

        public virtual ICollection<Character> Characters { get; set; }
    }
}