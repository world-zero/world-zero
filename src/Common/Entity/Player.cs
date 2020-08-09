using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.Entity;

namespace WorldZero.Common.Entity
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

        /// <summary>
        /// IsBlocked controls whether or not a Player can sign into any of
        /// their characters.
        /// </summary>
        public bool IsBlocked { get; set; }
    }
}