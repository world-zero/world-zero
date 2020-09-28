using WorldZero.Common.ValueObject;
using WorldZero.Data.Interface.Repository.Entity.RAM.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="IFoeRepo"/>
    public class RAMFoeRepo
        : IRAMIdIdRepo<Foe>,
          IFoeRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Foe(new Id(3), new Id(2));
            return a.GetUniqueRules().Count;
        }
    }
}