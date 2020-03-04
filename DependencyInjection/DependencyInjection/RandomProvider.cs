using System;

namespace DependencyInjection
{
    public class RandomProvider : IRandomProvider
    {
        private static readonly Random random = new Random();

        public int Next(int min, int max) => random.Next(min, max + 1);
    }
}
