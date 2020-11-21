using System;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Interface.Entity.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <summary>
    /// This relation applies a vote from the supplied character to the
    /// corresponding praxis.
    /// </summary>
    /// <remarks>
    /// While making sure that a player isn't voting on their own character
    /// requires knowing all of the player's character IDs, which is firmly the
    /// responsiblity of VoteReg, this can perform a basic check to ensure that
    /// a character isn't voting for themself.
    /// </remarks>
    public class Vote : IIdIdRelation
    {
        /// <remarks>
        /// Changing this will NOT check existing instances of Vote to make
        /// sure that none of them do not step out of the new bounds.
        /// </remarks>
        public static PointTotal MinPoints
        {
            get { return _minPoints; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("MinPoints");
                if (value.Get > MaxPoints.Get)
                    throw new ArgumentException("MinPoints cannot be larger than MaxPoints.");
                _minPoints = value;
            }
        }
        private static PointTotal _minPoints = new PointTotal(1);

        /// <remarks>
        /// Changing this will NOT check existing instances of Vote to make
        /// sure that none of them do not step out of the new bounds.
        /// </remarks>
        public static PointTotal MaxPoints
        {
            get { return _maxPoints; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("MaxPoints");
                if (value.Get < MinPoints.Get)
                    throw new ArgumentException("MaxPoints cannot be smaller than MinPoints.");
                _maxPoints = value;
            }
        }
        private static PointTotal _maxPoints = new PointTotal(5);

        /// <summary>
        /// This is the character that is receiving the vote points.
        /// </summary>
        /// <remarks>
        /// This will not allow a character to vote for themself.
        /// </remarks>
        public Id ReceivingCharacterId
        {
            get { return this._receivingCharacterId; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("ReceivingCharacterId");
                if (   (this.VotingCharacterId != null)
                    && (value == this.VotingCharacterId)   )
                {
                    throw new ArgumentException("A character cannot vote for themself.");
                }
                this._receivingCharacterId = value;
            }
        }
        private Id _receivingCharacterId;

        /// <summary>
        /// VotingCharacterId is a wrapper for LeftId.
        /// </summary>
        /// <remarks>
        /// This will not allow a character to vote for themself.
        /// </remarks>
        public Id VotingCharacterId
        {
            get { return this.LeftId; }
            set
            {
                if (value == null)
                    throw new ArgumentException("VotingCharacterId");
                if (   (this.ReceivingCharacterId != null)
                    && (value == this.ReceivingCharacterId)   )
                {
                    throw new ArgumentException("A character cannot vote for themself.");
                }
                this.LeftId = value;
            }
        }

        /// <summary>
        /// PraxisId is a wrapper for RightId.
        /// </summary>
        public Id PraxisId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public Vote(
            Id votingCharacterId,
            Id praxisId,
            Id receivingCharacterId,
            PointTotal points
        )
            : base(votingCharacterId, praxisId)
        {
            this.ReceivingCharacterId = receivingCharacterId;
            this.Points = points;
        }

        public Vote(
            Id id,
            Id votingCharacterId,
            Id praxisId,
            Id receivingCharacterId,
            PointTotal points
        )
            : base(id, votingCharacterId, praxisId)
        {
            this.ReceivingCharacterId = receivingCharacterId;
            this.Points = points;
        }

        public Vote(
            RelationDTO<Id, int, Id, int> dto,
            Id receivingCharacterId,
            PointTotal points
        )
            : base(dto.LeftId, dto.RightId)
        {
            this.ReceivingCharacterId = receivingCharacterId;
            this.Points = points;
        }

        public Vote(
            Id id,
            RelationDTO<Id, int, Id, int> dto,
            Id receivingCharacterId,
            PointTotal points
        )
            : base(id, dto.LeftId, dto.RightId)
        {
            this.ReceivingCharacterId = receivingCharacterId;
            this.Points = points;
        }

        internal Vote(
            int id,
            int votingCharacterId,
            int praxisId,
            int receivingCharacterId,
            int points
        )
            : base(new Id(id), new Id(votingCharacterId), new Id(praxisId))
        {
            try
            {
                this.ReceivingCharacterId = new Id(receivingCharacterId);
                this.Points = new PointTotal(points);
            }
            catch (ArgumentException)
            {
                throw new InvalidOperationException("While initializing a Vote from the Dapper-intended constructor, invalid data was discovered.");
            }
        }

        public override IEntity<Id, int> Clone()
        {
            return new Vote(
                this.Id,
                this.LeftId,
                this.RightId,
                this.ReceivingCharacterId,
                this.Points
            );
        }

        public PointTotal Points
        {
            get { return this._points; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Points");
                if (MinPoints == null)
                    throw new InvalidOperationException("MinPoints should not be null, something has corrupted it.");
                if (MaxPoints == null)
                    throw new InvalidOperationException("MaxPoints should not be null, something has corrupted it.");
                if (MinPoints.Get > MaxPoints.Get)
                    throw new InvalidOperationException("MinPoints is greater than MaxPoints, this should have been caught earlier.");
                if (value.Get < MinPoints.Get)
                    throw new ArgumentException($"The point total {value.Get} is below the minimum, {MinPoints.Get}.");
                if (value.Get > MaxPoints.Get)
                    throw new ArgumentException($"The point total {value.Get} is above the maximum, {MaxPoints.Get}.");

                this._points = value;
            }
        }
        protected PointTotal _points;
    }
}