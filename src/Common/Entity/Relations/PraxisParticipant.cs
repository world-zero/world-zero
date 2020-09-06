using System;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation maps a character's ID to another character's ID,
    /// signifying that they are collaberating on a.
    /// </summary>
    /// </remarks>
    /// PraxisParticipant repositories are responsible for ensuring that there
    /// is a triple uniqueness on PraxisId, CharacterId, and SubmissionCount,
    /// whereas the PraxisParticipant creation service class is responsible for
    /// ensuring that the character can actually have that number of
    /// submissions for that praxis at that level.
    /// </remarks>
    public class PraxisParticipant : IIdIdRelation
    {
        /// <summary>
        /// PraxisId wraps LeftId, which is the ID of the related Praxis.
        /// </summary>
        public Id PraxisId
        {
            get { return this.LeftId; }
            set { this.LeftId = value; }
        }

        /// <summary>
        /// CharacterId wraps LeftId, which is the ID of the related Character.
        /// </summary>
        public Id CharacterId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public PraxisParticipant(
            Id praxisId,
            Id characterId,
            int submissionCount=1
        )
            : base(praxisId, characterId)
        {
            this.SubmissionCount = submissionCount;
        }

        public PraxisParticipant(
            Id id,
            Id praxisId,
            Id characterId,
            int submissionCount=1
        )
            : base(id, praxisId, characterId)
        {
            this.SubmissionCount = submissionCount;
        }

        internal PraxisParticipant(
            int id,
            int praxisId,
            int characterId,
            int submissionCount
        )
            : base(new Id(id), new Id(praxisId), new Id(characterId))
        {
            try
            {
                this.SubmissionCount = submissionCount;
            }
            catch (ArgumentException)
            {
                throw new InvalidOperationException("While initializing a PraxisParticipant from the Dapper-intended constructor, invalid data was discovered.");
            }
        }

        public override IEntity<Id, int> DeepCopy()
        {
            return new PraxisParticipant(
                this.Id,
                this.LeftId,
                this.RightId,
                this.SubmissionCount
            );
        }

        /// <summary>
        /// This value tracks the count of praxises this character has
        /// submitted. This is especially necessary for abilities that allow
        /// users to re-attempt or re-complete a task.
        /// </summary>
        public int SubmissionCount
        {
            get { return this._submissionCount; }
            set
            {
                if (value < 1)
                    throw new ArgumentException("A character cannot complete a praxis less than 1 times.");
                this._submissionCount = value;
            }
        }
        protected int _submissionCount;
    }
}