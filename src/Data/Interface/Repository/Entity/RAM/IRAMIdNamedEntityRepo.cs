using System;
using System.Linq;
using System.Collections.Generic;
using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.Entity;

// NOTE: The logic for enforcing a name uniqueness is repeated to enforce a
// unique composite "key" of Left/Right IDs in IRAMEntityRelationRepo. Any
// changes that need to be applied to this class are likely needed there as
// well.

namespace WorldZero.Data.Interface.Repository.Entity.RAM
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
                from e in this._saved.Values
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
    }
}
