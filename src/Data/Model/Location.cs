using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Location")]
    public class Location : IModel
    {
        [NotMapped]
        private Id _locationId;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        private Name _city;
        [Required]
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
        private Name _state;
        [Required]
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
        private Name _country;
        [Required]
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
        private Name _zip;
        [Required]
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

        public virtual ICollection<Character> Characters { get; set; }
    }
}