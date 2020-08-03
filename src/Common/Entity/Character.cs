using WorldZero.Common.Interface;
using WorldZero.Common.Interface.Entity;
using WorldZero.Common.Entity.Mappings;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Entity
{
    [Table("Character")]
    /// <summary>
    /// Character is a entity for a tuple of the Character table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class Character : IIdNamedEntity
    {
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
        public bool HasBio { get; set; } = false;

        /// <summary>
        /// HasProfilePic is used to record if a character has a profile
        /// picture or not.
        /// </summary>
        public bool HasProfilePic { get; set; } = false;

        [Required]
        /// <summary>
        /// PlayerId is a wrapper for an <c>Id</c> - no exceptions are
        /// caught.
        /// </summary>
        public virtual int PlayerId
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._playerId,
                    0);
            }
            set { this._playerId = new Id(value); }
        }
        [NotMapped]
        private Id _playerId;

        [ForeignKey("PlayerId")]
        internal virtual Player Player { get; set; }

        [Required]
        /// <summary>
        /// EraPoints is a wrapper for a <c>PointTotal</c> - no exceptions are
        /// caught.
        /// </summary>
        public int EraPoints
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._eraPoints,
                    0);
            }
            set { this._eraPoints = new PointTotal(value); }
        }
        [NotMapped]
        private PointTotal _eraPoints;

        [Required]
        /// <summary>
        /// TotalPoints is a wrapper for a <c>PointTotal</c> - no exceptions are
        /// caught.
        /// </summary>
        public int TotalPoints
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._totalPoints,
                    0);
            }
            set { this._totalPoints = new PointTotal(value); }
        }
        [NotMapped]
        private PointTotal _totalPoints;

        [Required]
        /// <summary>
        /// EraLevel is a wrapper for a <c>Level</c>, which is dynamically
        /// determined on call by checking EraPoints. The setter does nothing.
        /// </summary>
        public int EraLevel
        {
            get
            {
                PointTotal p = new PointTotal(this.EraPoints);
                return this._calculateLevel(p).Get;
            }
            set { }
        }

        [Required]
        /// <summary>
        /// TotalLevel is a wrapper for a <c>Level</c>, which is dynamically
        /// determined on call by checking TotalPoints. The setter does
        /// nothing.
        /// </summary>
        public int TotalLevel
        {
            get
            {
                PointTotal p = new PointTotal(this.TotalPoints);
                return this._calculateLevel(p).Get;
            }
            set { }
        }

        [Required]
        /// <summary>
        /// VotePointsLeft is a wrapper for a <c>PointTotal</c> - no exceptions are
        /// caught.
        /// </summary>
        public int VotePointsLeft
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._votePointsLeft,
                    100);
            }
            set { this._votePointsLeft = new PointTotal(value); }
        }
        [NotMapped]
        private PointTotal _votePointsLeft;

        /// <summary>
        /// LocationId is a wrapper for an <c>Id</c> - no exceptions are
        /// caught.
        /// </summary>
        public virtual int? LocationId
        {
            get
            {
                int r = this.Eval<int>(
                    (ISingleValueObject<int>) this._locationId,
                    -1);
                if (r == -1) return null;
                else         return r;
            }
            set
            {
                if (value != null)
                    this._locationId = new Id((int) value);
                else
                    this._locationId = null;
            }
        }
        [NotMapped]
        private Id _locationId;

        [ForeignKey("LocationId")]
        internal virtual Location Location { get; set; }

        /// <summary>
        /// FactionId is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
        public virtual string FactionId
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._factionId,
                    null);
            }
            set
            {
                if (value == null) this._factionId = null;
                else               this._factionId = new Name(value);
            }
        }
        [NotMapped]
        private Name _factionId;

        [ForeignKey("FactionId")]
        internal virtual Faction Faction { get; set; }

        // These relations are handled via Fluent API.
        //internal virtual ICollection<Character> Friends { get; set; }
        internal virtual ICollection<FriendMap> Friends { get; set; }
        internal virtual ICollection<Character> Foes { get; set; }

        internal virtual ICollection<Praxis> Praxises { get; set; }
    }
}