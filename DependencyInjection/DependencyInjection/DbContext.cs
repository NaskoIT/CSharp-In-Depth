using System;
using System.Collections.Generic;

namespace DependencyInjection
{
    public class DbContext : IDbContext
    {
        private readonly string connectionString;

        public DbContext(IAppSettings appSettings)
        {
            connectionString = appSettings.ConnectionString;
        }

        public List<Cat> GetCats() =>
            new List<Cat>
            {
                new Cat(1, "cat1", DateTime.Now),
                new Cat(2, "cat2", DateTime.Now.AddDays(-1)),
                new Cat(3, "cat3", DateTime.Now.AddHours(-1)),
                new Cat(4, "cat4", DateTime.Now.AddSeconds(-20)),
                new Cat(5, "cat5", DateTime.Now.AddMonths(-1)),
            };
    }
}