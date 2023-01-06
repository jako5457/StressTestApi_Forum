namespace StressTestApi_Forum.Services
{
    public interface IFakeJitterService
    {
        public int RandomDelay { get; }

        FakeErrorResponse GetErrorResponse();
        bool HasError();
    }
}