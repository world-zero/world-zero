using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    public abstract class IUnsafeNamedProxy<TEntity>
        : IUnsafeProxy<TEntity, Name, string>
        where TEntity : class, IUnsafeEntity, INamedEntity
    {
        public IUnsafeNamedProxy(TEntity e)
            : base(e)
        { }
    }
}