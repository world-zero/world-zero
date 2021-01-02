using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Common.Interface.Entity.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="IPlayer"/>
    public class UnsafePlayer : ABCIdNamedEntity, IPlayer
    {
        public UnsafePlayer(Name name, bool isBlocked=false)
            : base (name)
        {
            this.IsBlocked = isBlocked;
        }

        public UnsafePlayer(Id id, Name name, bool isBlocked=false)
            : base (id, name)
        {
            this.IsBlocked = isBlocked;
        }

        internal UnsafePlayer(int id, string name, bool isBlocked)
            : base (new Id(id), new Name(name))
        {
            this.IsBlocked = isBlocked;
        }

        public override IEntity<Id, int> CloneAsEntity()
        {
            return new UnsafePlayer(
                this.Id,
                this.Name,
                this.IsBlocked
            );
        }

        public bool IsBlocked { get; set; }
    }
}