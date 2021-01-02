using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Unspecified.Primary;
using WorldZero.Data.Interface.Repository.Entity.Generic;

namespace WorldZero.Data.Interface.Repository.Entity.RAM.Generic
{
    /// <inheritdoc cref="IRAMEntityRepo"/>
    /// <remarks>
    /// If an entity with a non-repo set ID is supplied, then there will be
    /// undefined behavior.
    /// </remarks>
    public abstract class IRAMIdEntityRepo<TEntity>
        : IRAMEntityRepo<TEntity, Id, int>,
          IIdEntityRepo<TEntity>
        where TEntity : class, IIdEntity
    {
        // This is set to 1 to mimic the default first value of
        // auto-generated database int IDs.
        protected static int _nextIdValue = 1;

        public IRAMIdEntityRepo()
            : base()
        { }

        protected override Id GenerateId(TEntity entity)
        {
            return new Id(_nextIdValue++);
        }
    }
}