namespace StressTestApi_Forum.Services.FakeJitter
{
    public interface IFakeJitterService
    {
        public int RandomDelay { get; }

        FakeErrorResponse GetErrorResponse();
        bool HasError();
    }
}