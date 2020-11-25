using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <summary>
    /// Player is a entity for a tuple of the Player table.
    /// </summary>
    public class Player : IIdNamedEntity
    {
        public Player(Name name, bool isBlocked=false)
            : base (name)
        {
            this.IsBlocked = isBlocked;
        }

        public Player(Id id, Name name, bool isBlocked=false)
            : base (id, name)
        {
            this.IsBlocked = isBlocked;
        }

        internal Player(int id, string name, bool isBlocked)
            : base (new Id(id), new Name(name))
        {
            this.IsBlocked = isBlocked;
        }

        public override IEntity<Id, int> Clone()
        {
            return new Player(
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