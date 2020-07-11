using WorldZero.Common.ValueObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;

// For these, see TODO.txt for more.
// TODO: Deal with HasBio and HasProfilePicture.
// TODO: Add Faction and Location parameters to the constructor.

namespace WorldZero.Data.Model
{
    /*
    public class Character
    {
        public CharacterModel Model { get; private set; }

        public int CharacterId
        {
            get { return this.Model.CharacterId; }
            private set { this.Model.CharacterId = new Id(value).Get; }
        }
 
        public string Displayname
        {
            get { return this.Model.Displayname; }
            set { this.Model.Displayname = new Name(value).Get; }
        }

        public int EraPoints
        {
            get { return this.Model.EraPoints; }
            set { this.Model.EraPoints = new PointTotal(value).Get; }
        }

        public int TotalPoints
        {
            get { return this.Model.TotalPoints; }
            set { this.Model.TotalPoints = new PointTotal(value).Get; }
        }

        public int VotePointsLeft
        {
            get { return this.Model.VotePointsLeft; }
            set { this.Model.VotePointsLeft = new PointTotal(value).Get; }
        }

        public int EraLevel
        {
            get { return this._determineLevel(this.Model.EraPoints); }
        }

        public int TotalLevel
        {
            get { return this._determineLevel(this.Model.TotalPoints); }
        }

        private int _determineLevel(PointTotal points)
        {
            int p = points.Get;
            // TODO: this
            return 0;
        }

        private int _determineLevel(int points)
        {
            return this._determineLevel(new PointTotal(points));
        }

        /// <summary>
        /// This is the Player constructor used to generate a brand new player.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// If any parameter is invalid, this exception will be thrown. Of
        /// note, that includes a null player.
        /// </exception>
        /// <remarks>
        /// This class does not set the PlayerId or verify that the Username is
        /// unique, that is done by the repo - consult it for any questions
        /// regarding this functionality. PlayerId will be 0 until it is set to
        /// a usable, non-negative ID by the repo.
        /// </remarks>
        public Character(
            Player player,
            string displayname,
            bool hasBio=false,
            bool hasProfilePic=false,
            int eraPoints=0,
            int totalPoints=0,
            int eraLevel=0,
            int totalLevel=0,
            int votePointsLeft=100
            // TODO: Location
            // TODO: Faction
            )
        {
            if (player == null)
                throw new ArgumentException("A Character must have a Player.");

            this._constructor(new CharacterModel()
                {
                    CharacterId = 0,
                    Displayname = displayname,
                    HasBio = hasBio,
                    HasProfilePic = hasProfilePic,
                    EraPoints = eraPoints,
                    TotalPoints = totalPoints,
                    EraLevel = eraLevel,
                    TotalLevel = totalLevel,
                    VotePointsLeft = 100,
                    // TODO: Location
                    // TODO: Faction

                    Friends = new HashSet<CharacterModel>(),
                    Foes = new HashSet<CharacterModel>(),
                    Praxises = new HashSet<PraxisModel>()
                });
        }

        /// <summary>
        /// This is the internal constructor that is/should be used by the repo
        /// that needs to get the model from the database. It is not
        /// recommended to use this constructor in any other way. If
        /// model.Characters is null, it will be initialized as a HashSet.
        /// </summary>
        /// <param name="model">The model gathered from the Player repo.</param>
        /// <exception cref="ArgumentException">
        /// If any parameter is invalid, this exception will be thrown.
        /// </exception>
        public Character(CharacterModel model)
        {
            this._constructor(model);
        }

        private void _constructor(CharacterModel m)
        {
            new Id(m.CharacterId);
            new Name(m.Displayname);
            new PointTotal(m.EraPoints);
            new PointTotal(m.TotalPoints);
            if (   (this._determineLevel(m.EraPoints)   != m.EraLevel  )
                || (this._determineLevel(m.TotalPoints) != m.TotalLevel)   )
            {
                WarningException w =
                    new WarningException($"An incorrect point to level mapping has been discovered on character {m.CharacterId}. The old value is being replaced.");		
                Console.WriteLine(w.ToString());
                m.EraPoints = this._determineLevel(m.EraPoints);
                m.TotalPoints = this._determineLevel(m.TotalPoints);
            }

            if (m.Friends == null)
                m.Friends = new HashSet<CharacterModel>();
            if (m.Foes == null)
                m.Foes = new HashSet<CharacterModel>();
            if (m.Praxises == null)
                m.Praxises = new HashSet<PraxisModel>();
            this.Model = m;
        }
    }
    //*/
}