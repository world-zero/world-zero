using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <summary>
    /// An ability is something that faction(s) can have to give them some
    /// bonus.
    /// </summary>
    public class Ability : INamedEntity, IOptionalEntity
    {
        public Ability(Name name, string description)
            : base(name)
        {
            this.Description = description;
        }

        internal Ability(string name, string description)
            : base(new Name(name))
        {
            this.Description = description;
        }

        public string Description
        {
            get { return this._desc; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Attempted to set a `Description` to null or whitespace.");
                this._desc = value;
            }
        }
        private string _desc;

        public override IEntity<Name, string> Clone()
        {
            return new Ability(
                this.Id,
                this.Description
            );
        }
    }
}