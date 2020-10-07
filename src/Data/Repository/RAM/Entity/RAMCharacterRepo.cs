using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.Entity;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity;
using WorldZero.Data.Interface.Repository.RAM.Entity;

namespace WorldZero.Data.Repository.RAM.Entity
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
                from character in this._saved.Values
                where character.PlayerId == playerId
                select character;

            if (chars.Count() == 0)
                throw new ArgumentException($"There are no characters with PlayerId of {playerId.Get}");
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