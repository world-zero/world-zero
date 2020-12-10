using WorldZero.Common.ValueObject.General;
using WorldZero.Common.Interface.Entity.Primary;
using WorldZero.Common.Interface.Entity.Generic.Primary;

namespace WorldZero.Common.Entity.Primary
{
    /// <inheritdoc cref="IMetaTask"/>
    public sealed class MetaTask
        : IUnsafeIdStatusedProxy<UnsafeMetaTask>, IMetaTask
    {
        public MetaTask(UnsafeMetaTask mt)
            : base(mt)
        { }

        public override IEntity<Id, int> Clone()
        {
            return new MetaTask(this._unsafeEntity);
        }

        public string Description => this._unsafeEntity.Description;
        public bool IsFlatBonus => this._unsafeEntity.IsFlatBonus;
        public PointTotal Bonus => this._unsafeEntity.Bonus;
    }
}