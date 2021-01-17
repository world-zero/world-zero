using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.DTO;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.DTO.Entity.Unspecified.Primary;

namespace WorldZero.Common.DTO.Entity.Primary
{
    /// <inheritdoc cref="ICharacterDTO"/>
    public class CharacterDTO : IdNamedDTO, ICharacterDTO
    {
        public bool HasBio { get; }
        public bool HasProfilePic { get; }
        public Id PlayerId { get; }
        public PointTotal VotePointsLeft { get; }
        public PointTotal EraPoints { get; }
        public PointTotal TotalPoints { get; }
        public Level EraLevel { get; }
        public Level TotalLevel { get; }
        public Name FactionId { get; }
        public Id LocationId { get; }

        public CharacterDTO(
            Id id=null,
            Name name=null,
            bool hasBio=false,
            bool hasProfilePic=false,
            Id playerId=null,
            PointTotal votePointsLeft=null,
            PointTotal eraPoints=null,
            PointTotal totalPoints=null,
            Level eraLevel=null,
            Level totalLevel=null,
            Name factionId=null,
            Id locationId=null
        )
            : base(id, name)
        {
            this.HasBio = hasBio;
            this.HasProfilePic = hasProfilePic;
            this.PlayerId = playerId;
            this.VotePointsLeft = votePointsLeft;
            this.EraPoints = eraPoints;
            this.TotalPoints = totalPoints;
            this.EraLevel = eraLevel;
            this.TotalLevel = totalLevel;
            this.FactionId = factionId;
            this.LocationId = locationId;
        }

        public override object Clone()
        {
            return new CharacterDTO(
                this.Id,
                this.Name,
                this.HasBio,
                this.HasProfilePic,
                this.PlayerId,
                this.VotePointsLeft,
                this.EraPoints,
                this.TotalPoints,
                this.EraLevel,
                this.TotalLevel,
                this.FactionId,
                this.LocationId
            );
        }

        public override bool Equals(IDTO dto)
        {
            var c = dto as CharacterDTO;
            return
                c != null
                && base.Equals(c)
                && this.XOR(c.HasBio, this.HasBio)
                && this.XOR(c.PlayerId, this.PlayerId)
                && this.XOR(c.VotePointsLeft, this.VotePointsLeft)
                && this.XOR(c.EraPoints, this.EraPoints)
                && this.XOR(c.TotalPoints, this.TotalPoints)
                && this.XOR(c.EraLevel, this.EraLevel)
                && this.XOR(c.TotalLevel, this.TotalLevel)
                && this.XOR(c.FactionId, this.FactionId)
                && this.XOR(c.LocationId, this.LocationId);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int r = base.GetHashCode();
                r -= this.HasBio.GetHashCode();
                r += this.HasProfilePic.GetHashCode();
                if (this.PlayerId != null)
                    r *= this.PlayerId.GetHashCode();
                if (this.VotePointsLeft != null)
                    r *= this.VotePointsLeft.GetHashCode();
                if (this.EraPoints != null)
                    r *= this.EraPoints.GetHashCode();
                if (this.TotalPoints != null)
                    r *= this.TotalPoints.GetHashCode();
                if (this.EraLevel != null)
                    r *= this.EraLevel.GetHashCode();
                if (this.TotalLevel != null)
                    r *= this.TotalLevel.GetHashCode();
                if (this.FactionId != null)
                    r *= this.FactionId.GetHashCode();
                if (this.LocationId != null)
                    r *= this.LocationId.GetHashCode();
                return r;
            }
        }
    }
}