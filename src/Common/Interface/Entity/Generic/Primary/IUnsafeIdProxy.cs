using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    public abstract class IUnsafeIdProxy<TEntity>
        : IUnsafeProxy<TEntity, Id, int>
        where TEntity : class, IUnsafeEntity, IIdEntity
    {
        public IUnsafeIdProxy(TEntity e)
            : base(e)
        { }
    }
}