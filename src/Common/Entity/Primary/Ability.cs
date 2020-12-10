using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="IAbility"/>
    public sealed class Ability : IUnsafeNamedProxy<UnsafeAbility>, IAbility
    {
        public Ability(UnsafeAbility ability)
            : base(ability)
        { }

        public override IEntity<Name, string> Clone()
        {
            return new Ability(this._unsafeEntity);
        }

        public string Description => this._unsafeEntity.Description;
    }
}