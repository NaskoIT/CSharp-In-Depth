using System;

namespace DependencyInjection
{
    public interface IDateTimeProvider
    {
        DateTime Now();
    }
}
