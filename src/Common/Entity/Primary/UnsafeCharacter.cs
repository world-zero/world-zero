using System;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Common.ValueObject.General;

// TODO: be sure this doesn't shit itself when an unmatching level is supplied

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="ICharacter"/>
    public class UnsafeCharacter : ABCIdNamedEntity, ICharacter
    {
        /// <param name="locationId"></param>
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

        /// <remarks>
        /// The level fields are not stored as they are determined on 
        /// property request. That said, this will throw an exception if there
        /// is a supplied level that does not match the corresponding point
        /// totol.
        /// </remarks>
        internal UnsafeCharacter(
            int    id,
            string name,
            int    playerId,
            string factionId,
            int    locationId,
            double eraPoints,
            int    eraLevel,
            double totalPoints,
            int    totalLevel,
            int    votePointsLeft,
            bool   hasBio,
            bool   hasProfilePic
        )
            : base(new Id(id), new Name(name))
        {
            var expectedEraLevel =
                Level.CalculateLevel(new PointTotal(eraPoints)).Get;
            if (eraLevel != expectedEraLevel)
                throw new InvalidOperationException($"EraLevel ({eraLevel}) and EraPoints ({expectedEraLevel}) do not match.");
            var expectedTotalLevel =
                Level.CalculateLevel(new PointTotal(totalPoints)).Get;
            if (totalLevel != expectedTotalLevel)
                throw new InvalidOperationException($"TotalLevel ({totalLevel}) and TotalPoints ({expectedTotalLevel}) do not match.");

            this._setup(
                new Id(playerId),
                new Name(factionId),
                new Id(locationId),
                new PointTotal(eraPoints),
                new PointTotal(totalPoints),
                new PointTotal(votePointsLeft),
                hasBio,
                hasProfilePic
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

        public override IEntity<Id, int> CloneAsEntity()
        {
            return new UnsafeCharacter(
                this.Id,
                this.Name,
                this.PlayerId,
                this.FactionId,
                this.LocationId,
                this.EraPoints,
                this.TotalPoints,
                this.VotePointsLeft,
                this.HasBio,
                this.HasProfilePic
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