namespace StressTestApi_Forum.Services
{
    public interface IFakeJitterService
    {
        FakeErrorResponse GetErrorResponse();
        bool HasError();
    }
}