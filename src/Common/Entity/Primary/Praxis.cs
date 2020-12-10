using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="IPraxis"/>
    public sealed class Praxis
        : IUnsafeIdStatusedProxy<UnsafePraxis>, IPraxis
    {
        public Praxis(UnsafePraxis praxis)
            : base(praxis)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new Praxis(this._unsafeEntity);
        }

        public Id TaskId => this._unsafeEntity.TaskId;
        public PointTotal Points => this._unsafeEntity.Points;
        public Id MetaTaskId => this._unsafeEntity.MetaTaskId;
        public bool AreDueling => this._unsafeEntity.AreDueling;
    }
}