using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Common.Model
{
    [Table("Character")]
    /// <summary>
    /// Character is a model for a tuple of the Character table, with
    /// collections for it's various *-to-many relations.
    /// </summary>
    public class Character : IModel
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

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// CharacterId is a wrapper for an <c>Id</c> - no exceptions are
        /// caught.
        /// </summary>
        public int CharacterId
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._characterId,
                    0);
            }
            set { this._characterId = new Id(value); }
        }
        [NotMapped]
        private Id _characterId;

        [Required]
        /// <summary>
        /// Displayname is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
        public string Displayname
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._displayname,
                    null);
            }
            set { this._displayname = new Name(value); }
        }
        [NotMapped]
        private Name _displayname;

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
        /// <summary>
        /// The <c>Player</c> that owns this <c>Character</c>.
        /// </summary>
        public virtual Player Player { get; set; }

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
                // An ID cannot be negative, and since I am lazy and I don't
                // want to write a nullable ID value object, this will return
                // an invalid result on failure instead of the usual zero to
                // signify that the get accessor should return null.
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
        /// <summary>
        /// The <c>Location</c> that this <c>Character</c> lives in.
        /// </summary>
        public virtual Location Location { get; set; }

        [NotMapped]
        private Name _factionName;
        /// <summary>
        /// FactionName is a wrapper for a <c>Name</c> - no exceptions are
        /// caught.
        /// </summary>
        public virtual string FactionName
        {
            get
            {
                return this.Eval<string>(
                    (ISingleValueObject<string>) this._factionName,
                    null);
            }
            set
            {
                if (value == null) this._factionName = null;
                else               this._factionName = new Name(value);
            }
        }

        [ForeignKey("FactionName")]
        /// <summary>
        /// The <c>Faction</c> that this <c>Character</c> belongs to.
        /// </summary>
        public virtual Faction Faction { get; set; }

        // These relations are handled via Fluent API.
        public virtual ICollection<Character> Friends { get; set; }
        public virtual ICollection<Character> Foes { get; set; }

        public virtual ICollection<Praxis> Praxises { get; set; }
    }
}