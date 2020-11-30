using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;
using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.RAM.Generic
{
    /// <inheritdoc cref="IRAMIdEntityRepo"/>
    public abstract class IRAMIdStatusedEntityRepo<TEntity>
        : IRAMIdEntityRepo<TEntity>,
          IIdStatusedEntityRepo<TEntity>
        where TEntity : IIdStatusedEntity
    {
        public IRAMIdStatusedEntityRepo()
            : base()
        { }

        public void DeleteByStatusId(Name statusId)
        {
            if (statusId == null)
                throw new ArgumentNullException("statusId");

            IEnumerable<TEntity> entities =
                from e in this._saved.Values
                let entity = this.TEntityCast(e)
                where entity.StatusId == statusId
                select entity;

            foreach (TEntity e in entities)
                this.Delete(e.Id);
        }

        public async Task DeleteByStatusIdAsync(Name statusId)
        {
            this.DeleteByStatusId(statusId);
        }
    }
}