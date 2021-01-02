using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Service.Interface.Entity.Generic.Update
{
    /// <inheritdoc cref="IIdNamedEntityUpdate{TEntity}"/>
    public abstract class ABCIdNamedEntityUpdate<TEntity>
        : ABCEntityUpdate<TEntity, Id, int>,
        IIdNamedEntityUpdate<TEntity>
        where TEntity : class, IIdNamedEntity
    {
        public ABCIdNamedEntityUpdate(IIdNamedEntityRepo<TEntity> repo)
            : base(repo)
        { }

        public void AmendName(TEntity e, Name newName)
        {
            this.AssertNotNull(e, "e");
            this.AssertNotNull(newName, "newName");
            void f()
            {
                ABCIdNamedEntity n = (ABCIdNamedEntity) ((IIdNamedEntity) e);
                n.Name = newName;
            }
            this.AmendHelper(f, e);
        }

        public void AmendName(Id entityId, Name newName)
        {
            this.AssertNotNull(entityId, "entityId");
            this.AssertNotNull(newName, "newName");
            void f()
            {
                this.AmendName(this._repo.GetById(entityId), newName);
            }
            this.Transaction(f, true);
        }

        public async Task AmendNameAsync(TEntity e, Name newName)
        {
            this.AssertNotNull(e, "e");
            this.AssertNotNull(newName, "newName");
            await Task.Run(() => this.AmendName(e, newName));
        }

        public async Task AmendNameAsync(Id entityId, Name newName)
        {
            this.AssertNotNull(entityId, "entityId");
            this.AssertNotNull(newName, "newName");
            await Task.Run(() => this.AmendName(entityId, newName));
        }
    }
}