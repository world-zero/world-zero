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
        /// <summary>
        /// Initialize a new Faction. There is no requirement for any optional
        /// parameter to have another optional parameter. This means that it is
        /// possible to have a faction without an ability name but with an
        /// ability description.
        /// </summary>
        public Faction(
            Name name,
            PastDate dateFounded,
            string desc=null,
            Name abilityName=null
        )
            : base(name)
        {
            this._setup(
                dateFounded,
                desc,
                abilityName
            );
        }

        internal Faction(
            string name,
            DateTime dateFounded,
            string desc,
            string abilityName
        )
            : base(new Name(name))
        {
            this._setup(
                new PastDate(dateFounded),
                desc,
                new Name(abilityName)
            );
        }

        private void _setup(
            PastDate dateFounded,
            string desc,
            Name abilityName)
        {
            this.DateFounded = dateFounded;
            this.Description = desc;
            this.AbilityName = abilityName;
        }

        public override IEntity<Name, string> DeepCopy()
        {
            return new Faction(
                this.Id,
                this.DateFounded,
                this.Description,
                this.AbilityName
            );
        }

        public Name AbilityName { get; set; }

        public PastDate DateFounded
        {
            get { return this._dateFounded; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("DateFounded");
                this._dateFounded = value;
            }
        }
        private PastDate _dateFounded;

        public string Description
        {
            get { return this._description; }
            set
            {
                if (value == null)
                    this._description = null;
                else if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Attempted to set a `Description` to whitespace.");
                else
                    this._description = value;
            }
        }
        private string _description;
    }
}