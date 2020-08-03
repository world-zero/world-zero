using WorldZero.Common.ValueObject;

namespace WorldZero.Common.Interface.Entity
{
    // TODO: test this class, then make Friends, then EFCore-ize friends
    // TODO: then, after ^^, make foes and the rest of the intermediate children and grandchildren
    public abstract class IIdIdEntityMap : IEntityMap<Id, int, Id, int>
    {
        public override int LeftId
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._leftId,
                    0);
            }
            set { this._leftId = new Id(value); }
        }

        public override int RightId
        {
            get
            {
                return this.Eval<int>(
                    (ISingleValueObject<int>) this._rightId,
                    0);
            }
            set { this._rightId = new Id(value); }
        }
    }
}