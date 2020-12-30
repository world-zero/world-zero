using System.Threading.Tasks;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Update.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Entity.Update.Primary
{
    /// <inheritdoc cref="IStatusUpdate"/>
    public class StatusUpdate
        : ABCEntityUpdate<IStatus, Name, string>, IStatusUpdate
    {
        public StatusUpdate(IStatusRepo repo)
            : base(repo)
        { }

        public void AmendDescription(IStatus s, string newDesc)
        {
            this.AssertNotNull(s, "s");
            this.AssertNotNull(newDesc, "newDesc");
            void f() => ((UnsafeStatus) s).Description = newDesc;
            this.AmendHelper(f, s);
        }

        public void AmendDescription(Name statusId, string newDesc)
        {
            this.AssertNotNull(statusId, "statusId");
            this.AssertNotNull(newDesc, "newDesc");
            void f()
            {
                var s = this._repo.GetById(statusId);
                this.AmendDescription(s, newDesc);
            }
            this.Transaction(f, true);
        }

        public async Task AmendDescriptionAsync(IStatus f, string newDesc)
        {
            this.AssertNotNull(f, "f");
            this.AssertNotNull(newDesc, "newDesc");
            await Task.Run(() => this.AmendDescription(f, newDesc));
        }

        public async Task AmendDescriptionAsync(Name statusId, string newDesc)
        {
            this.AssertNotNull(statusId, "statusId");
            this.AssertNotNull(newDesc, "newDesc");
            await Task.Run(() => this.AmendDescription(statusId, newDesc));
        }
    }
}