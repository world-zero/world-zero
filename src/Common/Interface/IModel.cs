namespace WorldZero.Common.Interface
{
    public abstract class IModel
    {
        protected T Eval<T>(ISingleValueObject<T> svo, T other)
        {
            if (svo != null)
                return svo.Get;
            else
                return other;
        }
    }
}