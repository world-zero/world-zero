using WorldZero.Common.ValueObject.General;

namespace WorldZero.Common.Interface.Entity.Generic.Primary
{
    public abstract class IUnsafeIdEntity : IUnsafeEntity<Id, int>, IIdEntity
    {
        public IUnsafeIdEntity()
            : base(new Id(0))
        { }

        public IUnsafeIdEntity(Id id)
            : base(new Id(0))
        {
            this.Id = id;
        }
    }
}