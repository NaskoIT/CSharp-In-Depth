using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ExposeInternals.Tests")]

namespace ExposeInternals
{
    internal class Summator
    {
        internal long Sum(int a, int b)
        {
            var sum = (long)a + b;
            return sum;
        }

        private int GetZero()
        {
            return 0;
        }
    }
}
