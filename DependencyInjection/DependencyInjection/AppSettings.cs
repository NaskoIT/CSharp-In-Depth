namespace DependencyInjection
{
    public class AppSettings : IAppSettings
    {
        public string ConnectionString { get; } = "My connection string comes here";
    }
}
