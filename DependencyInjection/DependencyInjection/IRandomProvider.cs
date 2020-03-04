namespace DependencyInjection
{
    public interface IRandomProvider
    {
        int Next(int min, int max);
    }
}
