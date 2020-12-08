using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    public abstract class UnsafeIIdEntity : UnsafeIEntity<Id, int>, IIdEntity
    {
        public UnsafeIIdEntity()
            : base(new Id(0))
        { }

        public UnsafeIIdEntity(Id id)
            : base(new Id(0))
        {
            this.Id = id;
        }
    }
}