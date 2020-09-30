using System;
using System.Collections.Generic;
using System.Linq;
using WorldZero.Common.Interface;
using WorldZero.Common.ValueObject.General;
using WorldZero.Common.ValueObject.DTO.Entity.Relation;
using WorldZero.Common.Interface.Entity.Relation;
using WorldZero.Data.Interface.Repository.Entity.Relation;

namespace WorldZero.Data.Interface.Repository.Entity.RAM.Relation
{
    public abstract class IRAMEntityRelationCntRepo
    <
        TEntityRelation,
        TLeftId,
        TLeftBuiltIn,
        TRightId,
        TRightBuiltIn,
        TRelationDTO
    >
        : IRAMEntityRelationRepo
          <
            TEntityRelation,
            TLeftId,
            TLeftBuiltIn,
            TRightId,
            TRightBuiltIn,
            TRelationDTO
          >,
          IEntityRelationCntRepo
          <
            TEntityRelation,
            TLeftId,
            TLeftBuiltIn,
            TRightId,
            TRightBuiltIn,
            TRelationDTO
          >
        where TLeftId : ISingleValueObject<TLeftBuiltIn>
        where TRightId : ISingleValueObject<TRightBuiltIn>
        where TEntityRelation : IEntityRelationCnt
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
        where TRelationDTO : CntRelationDTO
            <TLeftId, TLeftBuiltIn, TRightId, TRightBuiltIn>
    {
        public IRAMEntityRelationCntRepo()
            : base()
        { }

        protected override void FinalChecks()
        {
            base.FinalChecks();
            // Ensure that the saved count values are correct.
            // Again, this is absurdly inefficient, but dev tool.
            foreach (KeyValuePair<Id, TEntityRelation> p in this._saved)
            {
                TEntityRelation er = p.Value;
                IEnumerable<TEntityRelation> partners =
                    from temp in this._saved.Values
                    where er.LeftId == temp.LeftId
                    where er.RightId == temp.RightId
                    select temp;

                int expected;
                if (partners.Count() == 0)
                {
                    expected = 1;
                }
                else
                {
                    // We do not care about the relation between Count and
                    // partners.Count() since entities can be deleted.
                    int largest = 0;
                    foreach (TEntityRelation temp in partners)
                    {
                        if (temp.Count > largest)
                            largest = temp.Count;
                    }
                    expected = largest + 1;
                }

                int c = er.Count;
                if (c != expected)
                    throw new ArgumentException($"The supplied entity relation does not have an expected Count of {expected}, instead having {c}.");
            }
        }
    }
}