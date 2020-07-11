using WorldZero.Common.Interface;
using WorldZero.Data.Model;
using WorldZero.Common.ValueObject;
using System;
using System.Collections.Generic;

namespace WorldZero.Common.Entity
{
    /*
    public class Player : IEntity
    {
        public PlayerModel Model { get; private set; }

        public int PlayerId
        {
            get { return this.Model.PlayerId; }
            private set { this.Model.PlayerId = new Id(value).Get; }
        }

        public string Username
        {
            get { return this.Model.Username; }
            set { this.Model.Username = new Name(value).Get; }
        }

        public bool IsBlocked
        {
            get { return this.Model.IsBlocked; }
            set { this.Model.IsBlocked = value; }
        }

        /// <summary>
        /// This is the Player constructor used to generate a brand new player.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// If any parameter is invalid, this exception will be thrown.
        /// </exception>
        /// <remarks>
        /// This class does not set the PlayerId or verify that the Username is
        /// unique, that is done by the repo - consult it for any questions
        /// regarding this functionality. PlayerId will be 0 until it is set to
        /// a usable, non-negative ID by the repo.
        /// </remarks>
        public Player(string username, bool isBlocked)
        {
            this._constructor(new PlayerModel()
                {
                    PlayerId = 0,
                    Username = username,
                    IsBlocked = isBlocked,
                    Characters = new HashSet<CharacterModel>()
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
        public Player(PlayerModel model)
        {
            this._constructor(model);
        }

        private void _constructor(PlayerModel m)
        {
            // The ID shouldn't be wrong, but it never hurts to check.
            new Id(m.PlayerId);
            new Name(m.Username);
            if (m.Characters == null)
                m.Characters = new HashSet<CharacterModel>();
            this.Model = m;
        }
    }
    //*/
}