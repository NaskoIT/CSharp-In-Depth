using System.Collections.Generic;

namespace DependencyInjection
{
    public interface IDbContext
    {
        List<Cat> GetCats();
    }
}
