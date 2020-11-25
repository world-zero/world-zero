using System;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Primary
{
    public class FactionUnset
        : IEntityUnset<Faction, Name, string, Character, Id, int>
    {
        public ICharacterRepo _charRepo
        { get { return (ICharacterRepo) this._otherRepo; } }

        public FactionUnset(IFactionRepo repo, ICharacterRepo charRepo)
            : base(repo, charRepo)
        { }

        public override void Unset(Name factionId)
        {
            this.AssertNotNull(factionId, "factionId");
            this.BeginTransaction();

            IEnumerable<Character> chars;
            try
            { chars = this._charRepo.GetByFactionId(factionId); }
            catch (ArgumentException e)
            {
                this.DiscardTransaction();
                throw new ArgumentException("Could not complete the unset.", e);
            }

            foreach (Character c in chars)
            {
                c.FactionId = null;
                try
                { this._charRepo.Update(c); }
                catch (ArgumentException e)
                {
                    this.DiscardTransaction();
                    throw new ArgumentException("Could not complete the unset.", e);
                }
            }

            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new InvalidOperationException("An error occurred during transaction end.", e); }
        }
    }
}