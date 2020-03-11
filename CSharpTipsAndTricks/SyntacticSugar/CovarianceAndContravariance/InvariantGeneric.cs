namespace CovarianceAndContravariance
{
    public class InvariantGeneric<T> : IInvariantGeneric<T>
    {
        public T Method(T parameter)
        {
            return parameter;
        }
    }
}
