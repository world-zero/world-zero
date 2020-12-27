using System.Threading.Tasks;
using WorldZero.Common.Entity.Primary;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Primary;
using WorldZero.Service.Interface.Entity.Update.Primary;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Entity.Update.Primary
{
    /// <inheritdoc cref="ITagUpdate"/>
    public class TagUpdate
        : ABCEntityUpdate<ITag, Name, string>, ITagUpdate
    {
        public TagUpdate(ITagRepo repo)
            : base(repo)
        { }

        public void AmendDescription(ITag t, string newDesc)
        {
            this.AssertNotNull(t, "t");
            this.AssertNotNull(newDesc, "newDesc");
            void f() => ((UnsafeTag) t).Description = newDesc;
            this.AmendHelper(f, t);
        }

        public void AmendDescription(Name tagId, string newDesc)
        {
            this.AssertNotNull(tagId, "tagId");
            this.AssertNotNull(newDesc, "newDesc");
            void f()
            {
                var t = this._repo.GetById(tagId);
                this.AmendDescription(t, newDesc);
            }
            this.Transaction(f, true);
        }

        public async Task AmendDescriptionAsync(ITag t, string newDesc)
        {
            this.AssertNotNull(t, "t");
            this.AssertNotNull(newDesc, "newDesc");
            await Task.Run(() => this.AmendDescription(t, newDesc));
        }

        public async Task AmendDescriptionAsync(Name tagId, string newDesc)
        {
            this.AssertNotNull(tagId, "tagId");
            this.AssertNotNull(newDesc, "newDesc");
            await Task.Run(() => this.AmendDescription(tagId, newDesc));
        }
    }
}