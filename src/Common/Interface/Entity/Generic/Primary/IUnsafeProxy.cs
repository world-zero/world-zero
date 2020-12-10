using System;
using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.General.Generic;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    /// <summary>
    /// This interface defines a proxy to an unsafe entity. For more, see <see
    /// cref="WorldZero.Common.Interface.Entity.Marker.IUnsafeEntity"/>.
    /// </summary>
    /// <remarks>
    /// Upon initialization, the supplied subject will have a deep copy
    /// performed and stored.
    /// <br />
    /// These proxies are responsible for making sure that the unsafe concrete
    /// entity that they proxy are not fiddled with without the use of the
    /// entity updating service classes - at least outside of the Data project
    /// and the various CRUD entity classes that need to update entities in
    /// order to perform their operation(s).
    /// <br />
    /// Classes that implement this should be sure to connect the inherited
    /// property getters to `_unsafeEntity`'s corresponding properties.
    /// </remarks>
    public abstract class IUnsafeProxy<TEntity, TId, TBuiltIn>
        : IEntity<TId, TBuiltIn>
        where TEntity : class, IUnsafeEntity, IEntity<TId, TBuiltIn>
        where TId : ISingleValueObject<TBuiltIn>
    {
        protected readonly TEntity _unsafeEntity;

        public IUnsafeProxy(TEntity e)
        {
            if (e == null)
                throw new ArgumentNullException("e");

            this._unsafeEntity = (TEntity) e.Clone();
        }

        /// <summary>
        /// This will return a clone of the stored unsafe entity.
        /// </summary>
        public TEntity CloneUnsafeEntity()
        {
            return (TEntity) this._unsafeEntity.Clone();
        }

        /// <remarks>
        /// Dev note: Clone() will just need to supply _unsafeEntity to the
        /// constructor of the to-be-returned proxy instance since the
        /// constructor already clones the unsafe entity.
        /// </remarks>
        public abstract IEntity<TId, TBuiltIn> Clone();

        public bool IsIdSet() => this._unsafeEntity.IsIdSet();
        public TId Id => this._unsafeEntity.Id;
    }
}