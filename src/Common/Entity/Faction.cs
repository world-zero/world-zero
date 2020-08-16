using WorldZero.Common.Interface.Entity;
using WorldZero.Common.ValueObject;
using System;

namespace WorldZero.Common.Entity
{
    /// <summary>
    /// Faction is a entity for a tuple of the Faction table.
    /// </summary>
    public class Faction : INamedEntity
    {
        public Faction(Name name, PastDate dateFounded,
            string desc=null, Name abilityName=null, string abilityDesc=null)
            : base(name)
        {
            this._setup(
                dateFounded,
                desc,
                abilityName,
                abilityDesc
            );
        }

        internal Faction(string name, DateTime dateFounded, string desc, string abilityName, string abilityDesc)
            : base(new Name(name))
        {
            this._setup(
                new PastDate(dateFounded),
                desc,
                new Name(abilityName),
                abilityDesc
            );
        }

        private void _setup(PastDate dateFounded, string desc,
                            Name abilityName, string abilityDesc)
        {
            this.DateFounded = dateFounded;
            this.Description = desc;
            this.AbilityName = abilityName;
            this.AbilityDesc = abilityDesc;
        }

        public override IEntity<Name, string> DeepCopy()
        {
            return new Faction(
                this.Id,
                this.DateFounded,
                this.Description,
                this.AbilityName,
                this.AbilityDesc
            );
        }

        public PastDate DateFounded
        {
            get { return this._dateFounded; }
            set
            {
                if (value == null)
                    throw new ArgumentException("Attempted to set a `PastDate` to null.");
                this._dateFounded = value;
            }
        }
        private PastDate _dateFounded;

        public string Description { get; set; }

        public Name AbilityName { get; set; }

        public string AbilityDesc { get; set; }
    }
}