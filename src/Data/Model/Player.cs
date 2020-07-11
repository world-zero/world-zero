using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Player")]
    public class Player : IModel
    {
        [NotMapped]
        private Id _playerId;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._playerId,
                    0);
            }
            set { this._playerId = new Id(value); }
        }

        [NotMapped]
        private Name _username;
        [Required, StringLength(25)]
        // Why, you may ask, is this limited to 25 characters? Because EF Core
        // was having a fit without a predefined length.
        // Also this field is Unique - enforced via Fluent API. Any future
        // repos must enforce this themselves.
        public string Username
        {
            get
            {
                // We can return a reference to this without worrying about the
                // user messing up the value to be greater than 25 characters
                // because ValueObjects are immutable and so are strings.
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._username,
                    null);
            }
            set
            {
                if (value == null)
                    this._username = null;
                else if (value.Length > 25)
                    throw new ArgumentException("A username cannot be longer than 25 characters.");
                else
                    this._username = new Name(value);
            }
        }

        [Required]
        public bool IsBlocked { get; set; } = false;

        public virtual ICollection<Character> Characters { get; set; }
    }
}