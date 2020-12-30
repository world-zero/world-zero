using System.Threading.Tasks;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Update.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Entity.Update.Primary
{
    /// <inheritdoc cref="IMetaTaskUpdate"/>
    public class MetaTaskUpdate
        : ABCIdStatusedEntityUpdate<IMetaTask>, IMetaTaskUpdate
    {
        public MetaTaskUpdate(IMetaTaskRepo repo, IStatusRepo statusRepo)
            : base(repo, statusRepo)
        { }

        // --------------------------------------------------------------------

        public void AmendDescription(IMetaTask mt, string newDesc)
        {
            this.AssertNotNull(mt, "mt");
            this.AssertNotNull(newDesc, "newDesc");
            void f() => ((UnsafeMetaTask) mt).Description = newDesc;
            this.AmendHelper(f, mt);
        }

        public void AmendDescription(Id mtId, string newDesc)
        {
            this.AssertNotNull(mtId, "mtId");
            this.AssertNotNull(newDesc, "newDesc");
            void g()
            {
                var f = this._repo.GetById(mtId);
                this.AmendDescription(f, newDesc);
            }
            this.Transaction(g, true);
        }

        public async Task AmendDescriptionAsync(IMetaTask mt, string newDesc)
        {
            this.AssertNotNull(mt, "mt");
            this.AssertNotNull(newDesc, "newDesc");
            await Task.Run(() => this.AmendDescription(mt, newDesc));
        }

        public async Task AmendDescriptionAsync(Id mtId, string newDesc)
        {
            this.AssertNotNull(mtId, "mtId");
            this.AssertNotNull(newDesc, "newDesc");
            await Task.Run(() => this.AmendDescription(mtId, newDesc));
        }

        // --------------------------------------------------------------------

        public void AmendBonus(IMetaTask mt, PointTotal newBonus)
        {
            this.AssertNotNull(mt, "mt");
            this.AssertNotNull(newBonus, "newBonus");
            void f() => ((UnsafeMetaTask) mt).Bonus = newBonus;
            this.AmendHelper(f, mt);
        }

        public void AmendBonus(Id mtId, PointTotal newBonus)
        {
            this.AssertNotNull(mtId, "mtId");
            this.AssertNotNull(newBonus, "newBonus");
            void g()
            {
                var f = this._repo.GetById(mtId);
                this.AmendBonus(f, newBonus);
            }
            this.Transaction(g, true);
        }

        public async Task AmendBonusAsync(IMetaTask mt, PointTotal newBonus)
        {
            this.AssertNotNull(mt, "mt");
            this.AssertNotNull(newBonus, "newBonus");
            await Task.Run(() => this.AmendBonus(mt, newBonus));
        }

        public async Task AmendBonusAsync(Id mtId, PointTotal newBonus)
        {
            this.AssertNotNull(mtId, "mtId");
            this.AssertNotNull(newBonus, "newBonus");
            await Task.Run(() => this.AmendBonus(mtId, newBonus));
        }

        // --------------------------------------------------------------------

        public void AmendIsFlatBonus(IMetaTask mt, bool newIsFlatBonus)
        {
            this.AssertNotNull(mt, "mt");
            this.AssertNotNull(newIsFlatBonus, "newIsFlatBonus");
            void f() => ((UnsafeMetaTask) mt).IsFlatBonus = newIsFlatBonus;
            this.AmendHelper(f, mt);
        }

        public void AmendIsFlatBonus(Id mtId, bool newIsFlatBonus)
        {
            this.AssertNotNull(mtId, "mtId");
            this.AssertNotNull(newIsFlatBonus, "newIsFlatBonus");
            void g()
            {
                var f = this._repo.GetById(mtId);
                this.AmendIsFlatBonus(f, newIsFlatBonus);
            }
            this.Transaction(g, true);
        }

        public async Task
        AmendIsFlatBonusAsync(IMetaTask mt, bool newIsFlatBonus)
        {
            this.AssertNotNull(mt, "mt");
            this.AssertNotNull(newIsFlatBonus, "newIsFlatBonus");
            await Task.Run(() => this.AmendIsFlatBonus(mt, newIsFlatBonus));
        }

        public async Task
        AmendIsFlatBonusAsync(Id mtId, bool newIsFlatBonus)
        {
            this.AssertNotNull(mtId, "mtId");
            this.AssertNotNull(newIsFlatBonus, "newIsFlatBonus");
            await Task.Run(() =>
                this.AmendIsFlatBonus(mtId, newIsFlatBonus));
        }
    }
}