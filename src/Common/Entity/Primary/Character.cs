using System;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Entity.Primary
{
    /// <summary>
    /// Character is a entity for a tuple of the Character table.
    /// </summary>
    public class Character : IIdNamedEntity, IEntityHasOptional
    {
        /// <summary>
        /// Determine the level based off the number of points supplied.
        /// </summary>
        /// <param name="points">The points to calculate the level of.</param>
        /// <returns><c>Level</c> corresponding to the <c>points</c>.</returns>
        public static Level CalculateLevel(PointTotal points)
        {
            if (points == null)
                throw new ArgumentNullException("points");

            int r = -1; // Just to make sure it's getting set.
            int p = points.AsInt;
            if      (p < 10)   r = 0;
            else if (p < 70)   r = 1;
            else if (p < 170)  r = 2;
            else if (p < 330)  r = 3;
            else if (p < 610)  r = 4;
            else if (p < 1090) r = 5;
            else if (p < 1840) r = 6;
            else if (p < 3040) r = 7;
            else               r = 8;
            try
            {
                return new Level(r);
            }
            catch (ArgumentException e)
            { throw new InvalidOperationException("This should not occur.", e); }
        }

        /// <param name="locationId"></param>
        /// <param name="eraPoints">If unspecified, this will be set to 0.</param>
        /// <param name="totalPoints">If unspecified, this will be set to 0.</param>
        /// <param name="votePointsLeft">If unspecified, this will be set to 100.</param>
        public Character(
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
        public Character(
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
        internal Character(
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
                CalculateLevel(new PointTotal(eraPoints)).Get;
            if (eraLevel != expectedEraLevel)
                throw new InvalidOperationException($"EraLevel ({eraLevel}) and EraPoints ({expectedEraLevel}) do not match.");
            var expectedTotalLevel =
                CalculateLevel(new PointTotal(totalPoints)).Get;
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

        public override IEntity<Id, int> Clone()
        {
            return new Character(
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

        /// <summary>
        /// HasBio is used to record if a character has a bio or not.
        /// </summary>
        public bool HasBio { get; set; }

        /// <summary>
        /// HasProfilePic is used to record if a character has a profile
        /// picture or not.
        /// </summary>
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
            get { return CalculateLevel(this.EraPoints); }
        }

        public Level TotalLevel
        {
            get { return CalculateLevel(this.TotalPoints); }
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