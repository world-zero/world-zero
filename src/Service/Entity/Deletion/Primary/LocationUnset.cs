using System;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IEntityUnset"/>
    public class LocationUnset
        : IEntityUnset<Location, Id, int, Character, Id, int>
    {
        protected ICharacterRepo _charRepo
        { get { return (ICharacterRepo) this._otherRepo; } }

        public LocationUnset(ILocationRepo repo, ICharacterRepo charRepo)
            : base(repo, charRepo)
        { }

        public override void Unset(Id locationId)
        {
            this.AssertNotNull(locationId, "locationId");
            this.BeginTransaction();

            IEnumerable<Character> chars = null;
            try
            { chars = this._charRepo.GetByLocationId(locationId); }
            catch (ArgumentException)
            { }

            if (chars != null)
            {
                foreach (Character c in chars)
                {
                    c.LocationId = null;
                    try
                    { this._charRepo.Update(c); }
                    catch (ArgumentException e)
                    {
                        this.DiscardTransaction();
                        throw new ArgumentException("Could not complete the unset.", e);
                    }
                }
            }

            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new InvalidOperationException("An error occurred during transaction end.", e); }
        }
    }
}