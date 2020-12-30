using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    public abstract class ABCIdEntity : ABCEntity<Id, int>, IIdEntity
    {
        public ABCIdEntity()
            : base(new Id(0))
        { }

        public ABCIdEntity(Id id)
            : base(new Id(0))
        {
            this.Id = id;
        }
    }
}