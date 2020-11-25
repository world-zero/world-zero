using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Data.Interface.Repository.RAM.Entity.Generic;

namespace WorldZero.Data.Repository.RAM.Entity.Primary
{
    /// <inheritdoc cref="ICharacterRepo"/>
    public class RAMCharacterRepo
        : IRAMIdNamedEntityRepo<Character>,
        ICharacterRepo
    {
        public IEnumerable<Character> GetByPlayerId(Id playerId)
        {
            if (playerId == null)
                throw new ArgumentNullException("playerId");

            IEnumerable<Character> chars =
                from c in this._saved.Values
                let character = this.TEntityCast(c)
                where character.PlayerId == playerId
                select character;

            if (chars.Count() == 0)
                throw new ArgumentException($"There are no characters with PlayerId of {playerId.Get}");
            else
                return chars;
        }

        public IEnumerable<Character> GetByLocationId(Id locationId)
        {
            if (locationId == null)
                throw new ArgumentNullException("locationId");

            IEnumerable<Character> chars =
                from c in this._saved.Values
                let character = this.TEntityCast(c)
                where character.LocationId != null
                where character.LocationId == locationId
                select character;

            if (chars.Count() == 0)
                throw new ArgumentException($"There are no characters with LocationId of {locationId}");
            else
                return chars;
        }

        public IEnumerable<Character> GetByFactionId(Name factionId)
        {
            if (factionId == null)
                throw new ArgumentNullException("factionId");

            IEnumerable<Character> chars =
                from c in this._saved.Values
                let character = this.TEntityCast(c)
                where character.FactionId != null
                where character.FactionId == factionId
                select character;

            if (chars.Count() == 0)
                throw new ArgumentException($"There are no characters with a Faction name of {factionId}");
            else
                return chars;
        }

        public Level FindHighestLevel(Player player)
        {
            if (player == null)
                throw new ArgumentNullException("player");
            return this.FindHighestLevel(player.Id);
        }

        public Level FindHighestLevel(Id playerId)
        {
            if (playerId == null)
                throw new ArgumentNullException("playerId");

            IEnumerable<Character> chars;
            try
            {
                chars = this.GetByPlayerId(playerId);
            }
            catch (ArgumentException e)
            { throw new ArgumentException(e.Message); }

            int highest = -1;
            foreach (Character c in chars)
            {
                int curr = c.TotalLevel.Get;
                if (curr > highest)
                    highest = curr;
                curr = c.EraLevel.Get;
                if (curr > highest)
                    highest = curr;
            }

            try
            {
                return new Level(highest);
            }
            catch (ArgumentException e)
            { throw new InvalidOperationException("This should not occur.", e); }
        }

        protected override int GetRuleCount()
        {
            var a = new Character(new Name("asdf"), new Id(2));
            return a.GetUniqueRules().Count;
        }
    }
}