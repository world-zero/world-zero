using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a character's ID to another character's ID,
    /// signifying that they are friends.
    /// </summary>
    public class Friend : IIdIdRelation
    {
        /// <summary>
        /// FirstCharacterId is a wrapper for RightId.
        /// </summary>
        public Id FirstCharacterId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        /// <summary>
        /// SecondCharacterId is a wrapper for RightId.
        /// </summary>
        public Id SecondCharacterId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public Friend(Id firstCharacterId, Id secondCharacterId)
            : base(firstCharacterId, secondCharacterId)
        { }

        public Friend(Id id, Id firstCharacterId, Id secondCharacterId)
            : base(id, firstCharacterId, secondCharacterId)
        { }

        internal Friend(int id, int firstCharacterId, int secondCharacterId)
            : base(
                new Id(id),
                new Id(firstCharacterId),
                new Id(secondCharacterId)
            )
        { }

        public override IEntity<Id, int> DeepCopy()
        {
            return new Friend(
                this.Id,
                this.LeftId,
                this.RightId
            );
        }
    }
}