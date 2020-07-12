using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldZero.Data.Model
{
    [Table("Character")]
    public class Character : IModel
    {
        [NotMapped]
        private Id _characterId;
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        private Name _displayname;
        [Required]
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

        public bool HasBio { get; set; } = false;
        public bool HasProfilePic { get; set; } = false;

        [NotMapped]
        private Id _playerId;
        [Required]
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
        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }

        [NotMapped]
        private PointTotal _eraPoints;
        [Required]
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
        private PointTotal _totalPoints;
        [Required]
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

        [Required]
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
        public int TotalLevel
        {
            get
            {
                PointTotal p = new PointTotal(this.TotalPoints);
                return this._calculateLevel(p).Get;
            }
            set { }
        }

        [NotMapped]
        private PointTotal _votePointsLeft;
        [Required]
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
        private Id _locationId;
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
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

        [NotMapped]
        private Name _factionName;
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
        public virtual Faction Faction { get; set; }

        // These relations are handled via Fluent API.
        public virtual ICollection<Character> Friends { get; set; }
        public virtual ICollection<Character> Foes { get; set; }

        public virtual ICollection<Praxis> Praxises { get; set; }


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
    }
}