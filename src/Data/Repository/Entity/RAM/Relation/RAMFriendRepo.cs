using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.RAM.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;
using WorldZero.Common.Entity.Relation;

namespace WorldZero.Data.Repository.Entity.RAM.Relation
{
    /// <inheritdoc cref="IFriendRepo"/>
    public class RAMFriendRepo
        : IRAMEntityRelationRepo
          <
            Friend,
            Id,
            int,
            Id,
            int,
            RelationDTO<Id, int, Id, int>
          >,
          IFriendRepo
    {
        protected override int GetRuleCount()
        {
            var a = new Friend(new Id(3), new Id(2));
            return a.GetUniqueRules().Count;
        }
    }
}