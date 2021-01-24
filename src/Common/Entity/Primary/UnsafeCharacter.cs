using System;
using WorldZero.Common.DTO.Entity.Primary;
using WorldZero.Common.Interface.DTO.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="ICharacter"/>
    public class UnsafeCharacter : ABCIdNamedEntity, ICharacter
    {
        /// <param name="eraPoints">If unspecified, this will be set to 0.</param>
        /// <param name="totalPoints">If unspecified, this will be set to 0.</param>
        /// <param name="votePointsLeft">If unspecified, this will be set to 100.</param>
        public UnsafeCharacter(
            Name       name,
            Id         playerId,
            Name       factionId    =null,
            Id         locationId     =null,
            PointTotal eraPoints      =null,
            PointTotal totalPoints    =null,
            PointTotal votePointsLeft =null,
            bool       hasBio         =false,
            bool       hasProfilePic  =false
        )
            : base(name)
        {
            this._setup(
                playerId,
                factionId,
                locationId,
                eraPoints,
                totalPoints,
                votePointsLeft,
                hasBio,
                hasProfilePic
            );
        }

        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="playerId"></param>
        /// <param name="factionId"></param>
        /// <param name="locationId"></param>
        /// <param name="eraPoints">If unspecified, this will be set to 0.</param>
        /// <param name="totalPoints">If unspecified, this will be set to 0.</param>
        /// <param name="votePointsLeft">If unspecified, this will be set to 100.</param>
        /// <param name="hasBio"></param>
        /// <param name="hasProfilePic"></param>
        public UnsafeCharacter(
            Id         id,
            Name       name,
            Id         playerId,
            Name       factionId      =null,
            Id         locationId     =null,
            PointTotal eraPoints      =null,
            PointTotal totalPoints    =null,
            PointTotal votePointsLeft =null,
            bool       hasBio         =false,
            bool       hasProfilePic  =false
        )
            : base(id, name)
        {
            this._setup(
                playerId,
                factionId,
                locationId,
                eraPoints,
                totalPoints,
                votePointsLeft,
                hasBio,
                hasProfilePic
            );
        }

        public UnsafeCharacter(ICharacterDTO dto)
            : base(dto.Id, dto.Name)
        {
            this._setup(
                dto.PlayerId,
                dto.FactionId,
                dto.LocationId,
                dto.EraPoints,
                dto.TotalPoints,
                dto.VotePointsLeft,
                dto.HasBio,
                dto.HasProfilePic
            );
        }

        private void _setup(
            Id         playerId,
            Name       factionId,
            Id         locationId,
            PointTotal eraPoints,
            PointTotal totalPoints,
            PointTotal votePointsLeft,
            bool       hasBio,
            bool       hasProfilePic
        )
        {
            this.PlayerId = playerId;
            this.FactionId = factionId;
            this.LocationId = locationId;
            this.EraPoints = eraPoints;
            this.TotalPoints = totalPoints;
            this.VotePointsLeft = votePointsLeft;
            this.HasBio = hasBio;
            this.HasProfilePic = hasProfilePic;
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

        public bool HasBio { get; set; }

        public bool HasProfilePic { get; set; }

        public Id PlayerId
        {
            get { return this._playerId; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("PlayerId");
                this._playerId = value;
            }
        }
        private Id _playerId;

        /// <value>
        /// If `null` is supplied, then the point total is set to 0.
        /// </value>
        public PointTotal EraPoints
        {
            get { return this._eraPoints; }
            set
            {
                if (value == null)
                    this._eraPoints = new PointTotal(0);
                else
                    this._eraPoints = value;
            }
        }
        private PointTotal _eraPoints;

        /// <value>
        /// If `null` is supplied, then the point total is set to 0.
        /// </value>
        public PointTotal TotalPoints
        {
            get { return this._totalPoints; }
            set
            {
                if (value == null)
                    this._totalPoints = new PointTotal(0);
                else
                    this._totalPoints = value;
            }
        }
        private PointTotal _totalPoints;

        public Level EraLevel
        {
            get { return Level.CalculateLevel(this.EraPoints); }
        }

        public Level TotalLevel
        {
            get { return Level.CalculateLevel(this.TotalPoints); }
        }

        /// <value>
        /// If `null` is supplied, then the point total is set to 100.
        /// </value>
        public PointTotal VotePointsLeft
        {
            get { return this._votePointsLeft; }
            set
            {
                if (value == null)
                    this._votePointsLeft = new PointTotal(100);
                else
                    this._votePointsLeft = value;
            }
        }
        private PointTotal _votePointsLeft;

        public Id LocationId { get; set; }

        public Name FactionId { get; set; }
    }
}