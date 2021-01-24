using System;
using WorldZero.Common.DTO.Entity.Relation;
using WorldZero.Common.Interface.DTO.Entity.Relation;
using WorldZero.Common.DTO.Entity.Unspecified.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.Interface.Entity.Unspecified.Relation;

namespace WorldZero.Common.Entity.Relation
{
    /// <inheritdoc cref="IVote"/>
    public class UnsafeVote : ABCEntityRelation<Id, int, Id, int>, IVote
    {
        public static PointTotal StaticMinPoints
        {
            get { return _minPoints; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("MinPoints");
                if (value.Get > StaticMaxPoints.Get)
                    throw new ArgumentException("MinPoints cannot be larger than MaxPoints.");
                _minPoints = value;
            }
        }
        private static PointTotal _minPoints = new PointTotal(1);

        public static PointTotal StaticMaxPoints
        {
            get { return _maxPoints; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("MaxPoints");
                if (value.Get < StaticMinPoints.Get)
                    throw new ArgumentException("MaxPoints cannot be smaller than MinPoints.");
                _maxPoints = value;
            }
        }
        private static PointTotal _maxPoints = new PointTotal(5);

        public PointTotal MinPoints { get { return StaticMinPoints; } }
        public PointTotal MaxPoints { get { return StaticMaxPoints; } }

        public Id CharacterId
        {
            get { return this.LeftId; }
            set
            {
                if (value == null)
                    throw new ArgumentException("VotingCharacterId");
                this.LeftId = value;
            }
        }

        public Id PraxisParticipantId
        {
            get { return this.RightId; }
            set { this.RightId = value; }
        }

        public UnsafeVote(
            Id characterId,
            Id praxisParticipantId,
            PointTotal points
        )
            : base(characterId, praxisParticipantId)
        {
            this.Points = points;
        }

        public UnsafeVote(
            Id id,
            Id characterId,
            Id praxisParticipantId,
            PointTotal points
        )
            : base(id, characterId, praxisParticipantId)
        {
            this.Points = points;
        }

        public UnsafeVote(
            NoIdRelationDTO<Id, int, Id, int> dto,
            PointTotal points
        )
            : base(dto.LeftId, dto.RightId)
        {
            this.Points = points;
        }

        public UnsafeVote(
            Id id,
            NoIdRelationDTO<Id, int, Id, int> dto,
            PointTotal points
        )
            : base(id, dto.LeftId, dto.RightId)
        {
            this.Points = points;
        }

        public UnsafeVote(IVoteDTO dto)
            : base(dto.Id, dto.LeftId, dto.RightId)
        {
            this.Points = dto.Points;
        }

        public override object Clone()
        {
            return new VoteDTO(
                this.Id,
                this.LeftId,
                this.RightId,
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

        public override NoIdRelationDTO<Id, int, Id, int> GetNoIdRelationDTO()
        {
            return new NoIdRelationDTO<Id, int, Id, int>(
                this.LeftId,
                this.RightId
            );
        }
    }
}