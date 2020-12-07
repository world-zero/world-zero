using System;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IEntityUnset"/>
    public class AbilityUnset
        : IEntityUnset<UnsafeAbility, Name, string, UnsafeFaction, Name, string>
    {
        protected IUnsafeFactionRepo _factionRepo
        { get { return (IUnsafeFactionRepo) this._otherRepo; } }

        public AbilityUnset(IUnsafeAbilityRepo repo, IUnsafeFactionRepo factionRepo)
            : base(repo, factionRepo)
        { }

        public override void Unset(Name factionId)
        {
            this.AssertNotNull(factionId, "factionId");
            this.BeginTransaction();

            IEnumerable<UnsafeFaction> factions = null;
            try
            { factions = this._factionRepo.GetByAbilityId(factionId); }
            catch (ArgumentException e)
            {
                this.DiscardTransaction();
                throw new ArgumentException("Could not complete the unset.", e);
            }

            if (factions != null)
            {
                foreach (UnsafeFaction c in factions)
                {
                    c.AbilityId = null;
                    try
                    { this._factionRepo.Update(c); }
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