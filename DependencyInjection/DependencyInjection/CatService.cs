using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjection
{
    public class CatService
    {
        public IEnumerable<CatResult> GetRandomCatsFromToday()
        {
            var database = new DbContext();
            var today = DateTime.Now;
            var startOfToday = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);

            var random = new Random();
            var totalCats = random.Next(10, 51);

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
