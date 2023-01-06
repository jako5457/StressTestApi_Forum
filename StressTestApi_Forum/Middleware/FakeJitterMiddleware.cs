using StressTestApi_Forum.Services;
using System.Text;

namespace StressTestApi_Forum.Middleware
{
    public class FakeJitterMiddleware : IMiddleware
    {
        //Enable Virtual delay 
        private const bool Delay = false;

        public IFakeJitterService _FakeJitterService;
        public FakeJitterMiddleware(IFakeJitterService fakeJitterService) {
            _FakeJitterService = fakeJitterService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (Delay)
            {
                await Task.Delay(_FakeJitterService.RandomDelay);
            }

            if (_FakeJitterService.HasError())
            {
                var error = _FakeJitterService.GetErrorResponse();

                context.Response.StatusCode = error.StatusCode;
                await context.Response.Body.FlushAsync();
                await context.Response.BodyWriter.WriteAsync(Encoding.UTF8.GetBytes(error.Message));
            }
            else
            {
                await next.Invoke(context);
            }
        }
    }
}
