using System;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.ValueObject;

namespace WorldZero.Common.Entity
{
    /// <summary>
    /// Character is a entity for a tuple of the Character table.
    /// </summary>
    public class Character : IIdNamedEntity
    {
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

        internal Character(
            int    id,
            string name,
            int    playerId,
            string factionId,
            int    locationId,
            int    eraPoints,
            int    totalPoints,
            int    votePointsLeft,
            bool   hasBio,
            bool   hasProfilePic
        )
            : base(new Id(id), new Name(name))
        {
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

        public override IEntity<Id, int> DeepCopy()
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
        /// Determine the level based off the number of points supplied.
        /// </summary>
        /// <param name="points">The points to calculate the level of.</param>
        /// <returns><c>Level</c> corresponding to the <c>points</c>.</returns>
        private Level _calculateLevel(PointTotal points)
        {
            int r = -1; // Just to make sure it's getting set.
            int p = points.Get;
            if      (p < 10)   r = 0;
            else if (p < 70)   r = 1;
            else if (p < 170)  r = 2;
            else if (p < 330)  r = 3;
            else if (p < 610)  r = 4;
            else if (p < 1090) r = 5;
            else if (p < 1840) r = 6;
            else if (p < 3040) r = 7;
            else               r = 8;
            return new Level(r);
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
            get { return this._calculateLevel(this.EraPoints); }
        }

        public Level TotalLevel
        {
            get { return this._calculateLevel(this.TotalPoints); }
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