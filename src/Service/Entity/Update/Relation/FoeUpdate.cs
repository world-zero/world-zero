using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Common.ValueObject.General;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Service.Interface.Entity.Update.Relation;
using WorldZero.Service.Interface.Entity.Generic.Update;

namespace WorldZero.Service.Entity.Update.Relation
{
    /// <inheritdoc cref="IFoeUpdate"/>
    public class FoeUpdate
        : ABCEntityUpdate<IFoe, Id, int>,
        IFoeUpdate
    {
        public FoeUpdate(IFoeRepo foeRepo)
            : base(foeRepo)
        { }
    }
}