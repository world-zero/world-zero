using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.ValueObject;

namespace WorldZero.Common.Interface.Entity.Unspecified.Relation
{
    /// <inheritdoc cref="IEntitySelfRelation"/>
    public abstract class ABCEntitySelfRelation<TId, TBuiltIn>
        : ABCEntityRelation<TId, TBuiltIn, TId, TBuiltIn>,
          IEntitySelfRelation<TId, TBuiltIn>
        where TId : ABCSingleValueObject<TBuiltIn>
    {
        public ABCEntitySelfRelation(TId leftId, TId rightId)
            : base(leftId, rightId)
        { }

        public ABCEntitySelfRelation(Id id, TId leftId, TId rightId)
            : base(id, leftId, rightId)
        { }
    }
}