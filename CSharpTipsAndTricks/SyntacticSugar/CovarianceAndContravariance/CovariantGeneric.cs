namespace CovarianceAndContravariance
{
    public class CovariantGeneric<T> : ICovariantGeneric<T>
    {
        public T Method()
        {
            return default(T);
        }
    }
}
