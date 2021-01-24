using WorldZero.Common.DTO.Entity.Primary;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.ValueObject.General;
using System;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="IFaction"/>
    public class UnsafeFaction : ABCNamedEntity, IFaction
    {
        /// <summary>
        /// Initialize a new Faction. There is no requirement for any optional
        /// parameter to have another optional parameter. This means that it is
        /// possible to have a faction without an ability name but with an
        /// ability description.
        /// </summary>
        public UnsafeFaction(
            Name name,
            PastDate dateFounded=null,
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

        public UnsafeFaction(IFactionDTO dto)
            : base(dto.Id)
        {
            this._setup(
                dto.DateFounded,
                dto.Description,
                dto.AbilityId
            );
        }

        private void _setup(
            PastDate dateFounded,
            string desc,
            Name abilityName)
        {
            if (dateFounded == null)
                dateFounded = new PastDate(DateTime.UtcNow);
            this.DateFounded = dateFounded;
            this.Description = desc;
            this.AbilityId = abilityName;
        }

        public override object Clone()
        {
            return new FactionDTO(
                this.Id,
                this.Description,
                this.DateFounded,
                this.AbilityId
            );
        }

        public Name AbilityId { get; set; }

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