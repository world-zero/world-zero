using System;
using System.Collections.Generic;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Interface.Entity.Update.Primary;
using WorldZero.Service.Interface.Entity.Deletion.Primary;

namespace WorldZero.Service.Entity.Deletion.Primary
{
    /// <inheritdoc cref="ILocationUnset"/>
    public class LocationUnset
        : ABCEntityUnset<ILocation, Id, int, ICharacter, Id, int>,
          ILocationUnset
    {
        protected ICharacterRepo _charRepo
        { get { return (ICharacterRepo) this._otherRepo; } }

        protected ICharacterUpdate _charUpdate
        { get { return (ICharacterUpdate) this._otherUpdate; } }

        public LocationUnset(
            ILocationRepo repo,
            ICharacterRepo charRepo,
            ICharacterUpdate characterUpdate
        )
            : base(repo, charRepo, characterUpdate)
        { }

        public override void Unset(Id locationId)
        {
            this.AssertNotNull(locationId, "locationId");
            this.BeginTransaction();

            IEnumerable<ICharacter> chars = null;
            try
            { chars = this._charRepo.GetByLocationId(locationId); }
            catch (ArgumentException)
            { }

            if (chars != null)
            {
                Id l = null;
                foreach (ICharacter c in chars)
                    this._charUpdate.AmendLocation(c, l);
            }

            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new InvalidOperationException("An error occurred during transaction end.", e); }
        }
    }
}