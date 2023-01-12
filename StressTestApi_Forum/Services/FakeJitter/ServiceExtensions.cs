using StressTestApi_Forum.Middleware;

namespace StressTestApi_Forum.Services.FakeJitter
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddFakeJitter(this IServiceCollection services)
        {
            services.AddSingleton<IFakeJitterService, FakeJitterService>();
            services.AddTransient<FakeJitterMiddleware>();
            return services;
        }

    }
}
