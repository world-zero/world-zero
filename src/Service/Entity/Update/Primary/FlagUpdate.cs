using System.Threading.Tasks;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Update.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Entity.Update.Primary
{
    /// <inheritdoc cref="IFlagUpdate"/>
    public class FlagUpdate : ABCEntityUpdate<IFlag, Name, string>, IFlagUpdate
    {
        public FlagUpdate(IFlagRepo repo)
            : base(repo)
        { }

        // --------------------------------------------------------------------

        public void AmendDescription(IFlag f, string newDesc)
        {
            this.AssertNotNull(f, "f");
            this.AssertNotNull(newDesc, "newDesc");
            void g() => ((UnsafeFlag) f).Description = newDesc;
            this.AmendHelper(g, f);
        }

        public void AmendDescription(Name flagId, string newDesc)
        {
            this.AssertNotNull(flagId, "flagId");
            this.AssertNotNull(newDesc, "newDesc");
            void g()
            {
                var f = this._repo.GetById(flagId);
                this.AmendDescription(f, newDesc);
            }
            this.Transaction(g, true);
        }

        public async Task AmendDescriptionAsync(IFlag f, string newDesc)
        {
            this.AssertNotNull(f, "f");
            this.AssertNotNull(newDesc, "newDesc");
            await Task.Run(() => this.AmendDescription(f, newDesc));
        }

        public async Task AmendDescriptionAsync(Name flagId, string newDesc)
        {
            this.AssertNotNull(flagId, "flagId");
            this.AssertNotNull(newDesc, "newDesc");
            await Task.Run(() => this.AmendDescription(flagId, newDesc));
        }

        // --------------------------------------------------------------------

        public void AmendPenalty(IFlag f, PointTotal newPenalty)
        {
            this.AssertNotNull(f, "f");
            this.AssertNotNull(newPenalty, "newPenalty");
            void g() => ((UnsafeFlag) f).Penalty = newPenalty;
            this.AmendHelper(g, f);
        }

        public void AmendPenalty(Name flagId, PointTotal newPenalty)
        {
            this.AssertNotNull(flagId, "flagId");
            this.AssertNotNull(newPenalty, "newPenalty");
            void g()
            {
                var f = this._repo.GetById(flagId);
                this.AmendPenalty(f, newPenalty);
            }
            this.Transaction(g, true);
        }

        public async Task AmendPenaltyAsync(IFlag f, PointTotal newPenalty)
        {
            this.AssertNotNull(f, "f");
            this.AssertNotNull(newPenalty, "newPenalty");
            await Task.Run(() => this.AmendPenalty(f, newPenalty));
        }

        public async Task AmendPenaltyAsync(Name flagId, PointTotal newPenalty)
        {
            this.AssertNotNull(flagId, "flagId");
            this.AssertNotNull(newPenalty, "newPenalty");
            await Task.Run(() => this.AmendPenalty(flagId, newPenalty));
        }

        // --------------------------------------------------------------------

        public void AmendIsFlatPenalty(IFlag f, bool newIsFlatPenalty)
        {
            this.AssertNotNull(f, "f");
            this.AssertNotNull(newIsFlatPenalty, "newIsFlatPenalty");
            void g() => ((UnsafeFlag) f).IsFlatPenalty = newIsFlatPenalty;
            this.AmendHelper(g, f);
        }

        public void AmendIsFlatPenalty(Name flagId, bool newIsFlatPenalty)
        {
            this.AssertNotNull(flagId, "flagId");
            this.AssertNotNull(newIsFlatPenalty, "newIsFlatPenalty");
            void g()
            {
                var f = this._repo.GetById(flagId);
                this.AmendIsFlatPenalty(f, newIsFlatPenalty);
            }
            this.Transaction(g, true);
        }

        public async Task
        AmendIsFlatPenaltyAsync(IFlag f, bool newIsFlatPenalty)
        {
            this.AssertNotNull(f, "f");
            this.AssertNotNull(newIsFlatPenalty, "newIsFlatPenalty");
            await Task.Run(() => this.AmendIsFlatPenalty(f, newIsFlatPenalty));
        }

        public async Task
        AmendIsFlatPenaltyAsync(Name flagId, bool newIsFlatPenalty)
        {
            this.AssertNotNull(flagId, "flagId");
            this.AssertNotNull(newIsFlatPenalty, "newIsFlatPenalty");
            await Task.Run(() =>
                this.AmendIsFlatPenalty(flagId, newIsFlatPenalty));
        }
    }
}