using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjection
{
    public class CatService
    {
        private readonly IDbContext database;
        private readonly IRandomProvider randomProvider;
        private readonly IDateTimeProvider dateTimeProvider;

        public CatService(IDbContext database, IRandomProvider randomProvider, IDateTimeProvider dateTimeProvider)
        {
            this.database = database;
            this.randomProvider = randomProvider;
            this.dateTimeProvider = dateTimeProvider;
        }

        public IEnumerable<CatResult> GetRandomCatsFromToday()
        {
            var today = dateTimeProvider.Now();
            var startOfToday = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);

            var random = new Random();
            var totalCats = randomProvider.Next(10, 50);

            var cats = database
                .GetCats()
                .Where(c => c.CreatedOn > startOfToday && c.CreatedOn < today)
                .Take(totalCats)
                .Select(c => new CatResult 
                {
                    Name = c.Name
                })
                .ToList();

            return cats;
        }
    }
}
