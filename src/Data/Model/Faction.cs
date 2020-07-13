using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Faction")]
    /// <summary>
    /// Faction is a model for a tuple of the Faction table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class Faction : IModel
    {
        [Key]
        /// <summary>
        /// FactionName is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
        public string FactionName
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._factionName,
                    null);
            }
            set { this._factionName = new Name(value); }
        }
        [NotMapped]
        private Name _factionName;

        /// <summary>
        /// DateFounded is a wrapper for a <c>PastDate</c> - no exceptions are
        /// caught.
        /// </summary>
        public DateTime DateFounded
        {
            get
            {
                return this.Eval<DateTime>(
                    (ISingleValueObject<DateTime>) this._dateFounded,
                    DateTime.UtcNow);
            }
            set { this._dateFounded = new PastDate(value); }
        }
        [NotMapped]
        private PastDate _dateFounded;

        /// <summary>
        /// Description is a description of the faction.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// AbilityName is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
        public string AbilityName
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._abilityName,
                    null);
            }
            set { this._abilityName = new Name(value); }
        }
        [NotMapped]
        private Name _abilityName;

        /// <summary>
        /// AbilityDesc is a description of the AbilityName.
        /// </summary>
        public string AbilityDesc { get; set; }

        public virtual ICollection<Character> Members { get; set; }
        public virtual ICollection<Task> SponsoredTasks { get; set; }
    }
}