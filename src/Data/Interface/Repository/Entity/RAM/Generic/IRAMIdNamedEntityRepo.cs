using WorldZero.Data.Interface.Repository.Entity.Primary.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Data.Interface.Repository.Entity.RAM.Generic
{
    /// <inheritdoc cref="IRAMEntityRepo"/>
    public abstract class IRAMIdNamedEntityRepo<TEntity>
        : IRAMIdEntityRepo<TEntity>,
          IIdNamedEntityRepo<TEntity>
        where TEntity : IIdNamedEntity
    {
        public IRAMIdNamedEntityRepo()
            : base()
        { }

        public TEntity GetByName(Name name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            IEnumerable<TEntity> match = 
                from eTemp in this._saved.Values
                let e = this.TEntityCast(eTemp)
                where e.Name.Equals(name)
                select e;

            var c = match.Count();
            if (c == 1)
                return (TEntity) match.First().Clone();
            else if (c == 0)
                throw new ArgumentException($"Could not find an entity with name {name.Get}.");
            else
                throw new InvalidOperationException($"Multiple names of {name.Get} found, which is a bug.");
        }

        public async Task<TEntity> GetByNameAsync(Name name)
        {
            return this.GetByName(name);
        }
    }
}
