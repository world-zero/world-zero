using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <summary>
    /// Player is a entity for a tuple of the Player table.
    /// </summary>
    public class UnsafePlayer : ABCIdNamedEntity, IUnsafeEntity
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

        public override ABCEntity<Id, int> Clone()
        {
            return new UnsafePlayer(
                this.Id,
                this.Name,
                this.IsBlocked
            );
        }

        /// <summary>
        /// IsBlocked controls whether or not a Player can sign into any of
        /// their characters.
        /// </summary>
        public bool IsBlocked { get; set; }
    }
}