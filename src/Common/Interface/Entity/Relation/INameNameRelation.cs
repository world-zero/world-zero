using WorldZero.Common.ValueObject;

namespace WorldZero.Common.Interface.Entity.Relation
{
    public abstract class INameNameRelation
        : IEntityRelation<Name, string, Name, string>
    {
        public INameNameRelation(Name leftId, Name rightId)
            : base(leftId, rightId)
        { }

        public INameNameRelation(Id id, Name leftId, Name rightId)
            : base(id, leftId, rightId)
        { }
    }
}