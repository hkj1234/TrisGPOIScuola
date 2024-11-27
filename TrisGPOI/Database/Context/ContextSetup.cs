namespace TrisGPOI.Database.Context
{
    public static class ContextSetup
    {
        public static IServiceCollection AddContext(this IServiceCollection services)
        {
            services.AddScoped<IDbContextFactory, DbContextFactory>();
            return services;
        }
    }
}
