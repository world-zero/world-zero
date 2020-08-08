using WorldZero.Common.ValueObject;
using WorldZero.Common.Interface.Entity;
using WorldZero.Data.Interface.Repository.Entity;

namespace WorldZero.Data.Repository.Entity
{
    /// <inheritdoc cref="IRAMEntityRepo"/>
    public abstract class IRAMIdEntityRepo<Entity>
        : IRAMEntityRepo<Entity, Id, int>,
          IIdEntityRepo<Entity>
        where Entity : IIdEntity
    {
        private int _nextIdValue;

        public IRAMIdEntityRepo()
            : base()
        {
            // This is set to 1 to mimic the default first value of
            // auto-generated database int IDs.
            this._nextIdValue = 1;
        }

        protected override Id GenerateId(Entity entity)
        {
            return new Id(this._nextIdValue++);
        }
    }
}