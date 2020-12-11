using System;
using System.Collections.Generic;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Generic.Deletion;

namespace WorldZero.Service.Entity.Deletion.Primary
{
    /// <inheritdoc cref="IEntityUnset"/>
    public class FactionUnset
        : IEntityUnset<UnsafeFaction, Name, string, UnsafeCharacter, Id, int>
    {
        protected ICharacterRepo _charRepo
        { get { return (ICharacterRepo) this._otherRepo; } }

        protected readonly TaskDel _taskDel;
        protected readonly MetaTaskUnset _mtUnset;

        public FactionUnset(
            IFactionRepo repo,
            ICharacterRepo charRepo,
            TaskDel taskDel,
            MetaTaskUnset mtUnset
        )
            : base(repo, charRepo)
        {
            this.AssertNotNull(taskDel, "taskDel");
            this.AssertNotNull(mtUnset, "mtUnset");

            this._taskDel = taskDel;
            this._mtUnset = mtUnset;
        }

        public override void Delete(Name factionId)
        {
            void f(Name name)
            {
                this._taskDel.DeleteByFaction(name);
                this._mtUnset.DeleteByFaction(name);
                base.Delete(name);
            }

            this.Transaction<Name>(f, factionId, true);
        }

        public override void Unset(Name factionId)
        {
            this.AssertNotNull(factionId, "factionId");
            this.BeginTransaction();

            IEnumerable<UnsafeCharacter> chars = null;
            try
            { chars = this._charRepo.GetByFactionId(factionId); }
            catch (ArgumentException)
            { }

            if (chars != null)
            {
                foreach (UnsafeCharacter c in chars)
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
            }

            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new InvalidOperationException("An error occurred during transaction end.", e); }
        }
    }
}