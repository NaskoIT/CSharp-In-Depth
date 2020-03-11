namespace CovarianceAndContravariance
{
    public interface ICovariantGeneric<out T>
    {
        // Invalid variance:
        // The type parameter 'T' must be contravariantly valid on 'ICovariantGeneric<T>.Method(T)'.
        // 'T' is covariant.
        //// T Method(T parameter);

        T Method();
    }
}
