using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    public abstract class IIdUnsafeProxy<TEntity>
        : IUnsafeProxy<TEntity, Id, int>
        where TEntity : class, IUnsafeEntity, IIdEntity
    {
        public IIdUnsafeProxy(TEntity e)
            : base(e)
        { }
    }
}