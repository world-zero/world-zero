using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    public abstract class IUnsafeIdNamedProxy<TEntity>
        : IUnsafeIdProxy<TEntity>
        where TEntity : class, IUnsafeEntity, IIdNamedEntity
    {
        public IUnsafeIdNamedProxy(TEntity e)
            : base(e)
        { }

        public Name Name => this._unsafeEntity.Name;
    }
}