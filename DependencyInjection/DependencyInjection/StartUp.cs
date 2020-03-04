using SimpleInjector;
using System;

namespace DependencyInjection
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var container = new Container();
            container.Register<IAppSettings, AppSettings>();
            container.Register<IDateTimeProvider, DateTimeProvider>();
            container.Register<IRandomProvider, RandomProvider>();
            container.Register<IDbContext, DbContext>();
            container.Verify();

            CatService catService = container.GetInstance<CatService>();

            foreach (var cat in catService.GetRandomCatsFromToday())
            {
                Console.WriteLine(cat.Name);
            }
        }
    }
}
