using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Faction")]
    public class Faction : IModel
    {
        [NotMapped]
        private Name _factionName;
        [Key]
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
        private PastDate _dateFounded;
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

        public string Description { get; set; }

        [NotMapped]
        private Name _abilityName;
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
        public string AbilityDesc { get; set; }

        public virtual ICollection<Character> Members { get; set; }
        public virtual ICollection<Task> SponsoredTasks { get; set; }
    }
}