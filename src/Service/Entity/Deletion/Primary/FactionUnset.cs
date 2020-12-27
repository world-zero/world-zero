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
    /// <inheritdoc cref="IFactionUnset"/>
    public class FactionUnset
        : ABCEntityUnset<IFaction, Name, string, ICharacter, Id, int>,
          IFactionUnset
    {
        protected ICharacterRepo _charRepo
        { get { return (ICharacterRepo) this._otherRepo; } }

        protected ICharacterUpdate _charUpdate
        { get { return (ICharacterUpdate) this._otherUpdate; } }

        protected readonly TaskDel _taskDel;
        protected readonly MetaTaskUnset _mtUnset;

        public FactionUnset(
            IFactionRepo repo,
            ICharacterRepo charRepo,
            ICharacterUpdate charUpdate,
            TaskDel taskDel,
            MetaTaskUnset mtUnset
        )
            : base(repo, charRepo, charUpdate)
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

            IEnumerable<ICharacter> chars = null;
            try
            { chars = this._charRepo.GetByFactionId(factionId); }
            catch (ArgumentException)
            { }

            if (chars != null)
            {
                Name f = null;
                foreach (ICharacter c in chars)
                    this._charUpdate.AmendFaction(c, f);
            }

            try
            { this.EndTransaction(); }
            catch (ArgumentException e)
            { throw new InvalidOperationException("An error occurred during transaction end.", e); }
        }
    }
}