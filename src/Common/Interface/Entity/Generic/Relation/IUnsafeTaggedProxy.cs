using WorldZero.Common.Interface.Entity.Marker;
using WorldZero.Common.Interface.General.Generic;
using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Relation
{
    public abstract class IUnsafeTaggedProxy
        <TEntity, TLeftId, TLeftBuiltIn>
        : IUnsafeRelationProxy
            <TEntity, TLeftId, TLeftBuiltIn, Name, string>
        where TEntity : class, IUnsafeEntity, ITaggedEntity
            <TLeftId, TLeftBuiltIn>
        where TLeftId : ISingleValueObject<TLeftBuiltIn>
    {
        public IUnsafeTaggedProxy(TEntity e)
            : base(e)
        { }

        public Name TagId => this._unsafeEntity.TagId;
    }
}