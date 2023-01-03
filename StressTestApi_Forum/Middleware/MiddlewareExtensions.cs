namespace StressTestApi_Forum.Middleware
{
    public static class MiddlewareExtensions
    {
         
        public static WebApplication UseFakeJitter(this WebApplication webApplication)
        {
            webApplication.UseMiddleware<FakeJitterMiddleware>();
            return webApplication;
        }


    }
}
