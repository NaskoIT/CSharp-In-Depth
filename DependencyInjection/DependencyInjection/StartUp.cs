using System;

namespace DependencyInjection
{
    class StartUp
    {
        static void Main(string[] args)
        {
            IDbContext dbContext = new DbContext(new AppSettings());
            IDateTimeProvider dateTimeProvider = new DateTimeProvider();
            IRandomProvider randomProvider = new RandomProvider();
            CatService catService = new CatService(dbContext, randomProvider, dateTimeProvider);

            foreach (var cat in catService.GetRandomCatsFromToday())
            {
                Console.WriteLine(cat.Name);
            }
        }
    }
}
