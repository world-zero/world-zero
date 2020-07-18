using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Model
{
    [Table("Player")]
    /// <summary>
    /// Player is a model for a tuple of the Player table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class Player : IModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// PlayerId is a wrapper for an <c>Id</c> - no exceptions are
        /// caught.
        /// </summary>
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
        private Id _playerId;

        [Required, StringLength(25)]
        /// <summary>
        /// Username is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
        /// <remarks>
        /// Why, you may ask, is this limited to 25 characters? Because EF Core
        /// was having a fit without a predefined length.
        /// Also this field is Unique - enforced via Fluent API. Any future
        /// repos must enforce this themselves.
        /// </remarks>
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
                if (value != null && value.Length > 25)
                    throw new ArgumentException("A username cannot be longer than 25 characters.");
                else
                    this._username = new Name(value);
            }
        }
        [NotMapped]
        private Name _username;

        [Required]
        /// <summary>
        /// IsBlocked controls whether or not a Player can sign into any of
        /// their characters.
        /// </summary>
        public bool IsBlocked { get; set; } = false;

        public virtual ICollection<Character> Characters { get; set; }
    }
}