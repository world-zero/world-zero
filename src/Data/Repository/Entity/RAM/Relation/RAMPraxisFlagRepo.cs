using WorldZero.Data.Interface.Repository.Entity.RAM.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="IPraxisFlagRepo">
    public class RAMPraxisFlagRepo
        : IRAMIdNameRepo<PraxisFlag>,
          IPraxisFlagRepo
    { }
}