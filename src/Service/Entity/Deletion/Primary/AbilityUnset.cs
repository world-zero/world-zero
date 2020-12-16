using System;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;
using WorldZero.Service.Interface.Entity.Deletion.Primary;

namespace WorldZero.Service.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IAbilityUnset"/>
    public class AbilityUnset
        : ABCEntityUnset<IAbility, Name, string, IFaction, Name, string>,
          IAbilityUnset
    {
        protected IFactionRepo _factionRepo
        { get { return (IFactionRepo) this._otherRepo; } }

        public AbilityUnset(IAbilityRepo repo, IFactionRepo factionRepo)
            : base(repo, factionRepo)
        { }

        public override void Unset(Name factionId)
        {
            this.AssertNotNull(factionId, "factionId");
            this.BeginTransaction();

            IEnumerable<IFaction> factions = null;
            try
            { factions = this._factionRepo.GetByAbilityId(factionId); }
            catch (ArgumentException e)
            {
                this.DiscardTransaction();
                throw new ArgumentException("Could not complete the unset.", e);
            }

            if (factions != null)
            {
                foreach (IFaction f in factions)
                {
                    ((UnsafeFaction) f).AbilityId = null;
                    try
                    { this._factionRepo.Update(f); }
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