using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    public abstract class IUnsafeIdStatusedProxy<TEntity>
        : IUnsafeIdProxy<TEntity>
        where TEntity : class, IUnsafeEntity, IIdStatusedEntity
    {
        public IUnsafeIdStatusedProxy(TEntity e)
            : base(e)
        { }

        public Name StatusId => this._unsafeEntity.StatusId;
    }
}