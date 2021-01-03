using System;
using System.Linq;
using System.Collections.Generic;
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

        public IEnumerable<TEntity> GetByStatusId(Name statusId)
        {
            if (statusId == null)
                throw new ArgumentNullException("statusId");

            IEnumerable<TEntity> entities =
                from e in this._saved.Values
                let entity = this.TEntityCast(e)
                where entity.StatusId == statusId
                select entity;

            if (entities.Count() == 0)
                throw new ArgumentException($"No entities exist with statusId ({statusId.Get}).");

            foreach (TEntity e in entities)
                yield return e;
        }
    }
}