using System;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="IAbility"/>
    public class UnsafeAbility : ABCNamedEntity, IAbility
    {
        public UnsafeAbility(Name name, string description)
            : base(name)
        {
            this.Description = description;
        }

        public UnsafeAbility(IAbilityDTO dto)
            : base(dto.Id)
        {
            this.Description = dto.Description;
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

        public override IEntity<Name, string> CloneAsEntity()
        {
            return new UnsafeAbility(
                this.Id,
                this.Description
            );
        }
    }
}