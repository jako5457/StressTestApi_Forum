using StressTestApi_Forum.Services;
using System.Text;

namespace StressTestApi_Forum.Middleware
{
    public class FakeJitterMiddleware : IMiddleware
    {
        public IFakeJitterService _FakeJitterService;
        public FakeJitterMiddleware(IFakeJitterService fakeJitterService) {
            _FakeJitterService = fakeJitterService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (_FakeJitterService.HasError())
            {
                var error = _FakeJitterService.GetErrorResponse();

                context.Response.StatusCode = error.StatusCode;
                await context.Response.Body.FlushAsync();
                await context.Response.BodyWriter.WriteAsync(Encoding.UTF8.GetBytes(error.Message));
            }
        }
    }
}
