namespace StressTestApi_Forum.Services
{
    public class FakeErrorResponse
    {
        public FakeErrorResponse(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public int StatusCode { get; private set; }

        public string Message { get; private set; } = string.Empty;

    }
}
