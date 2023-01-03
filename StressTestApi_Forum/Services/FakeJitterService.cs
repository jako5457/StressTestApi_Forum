using System.Security.Cryptography;

namespace StressTestApi_Forum.Services
{
    public class FakeJitterService : IFakeJitterService
    {
        const int occourance = 1;

        RandomNumberGenerator _RandomNumberGenerator;

        readonly List<FakeErrorResponse> fakeErrorResponses = new List<FakeErrorResponse>();

        public FakeJitterService()
        {
            _RandomNumberGenerator = RandomNumberGenerator.Create();

            fakeErrorResponses = new List<FakeErrorResponse>
            {
                new FakeErrorResponse(418,"Sorry. but you can't make coffee in a teapot."),
                new FakeErrorResponse(422,"Unprocessable Entity"),
                new FakeErrorResponse(429,"Too Many Requests"),
                new FakeErrorResponse(503,"Service Unavailable"),
                new FakeErrorResponse(415,"Unsupported Media Type"),
                new FakeErrorResponse(413,"Payload Too Large"),
                new FakeErrorResponse(500,"Internal Server Error"),
            };
        }

        public bool HasError()
        {
            byte[] data = new byte[100];
            _RandomNumberGenerator.GetBytes(data);

            int count = data.Where(b => b < occourance).Count();

            return count < occourance;
        }

        public FakeErrorResponse GetErrorResponse()
        {
            byte[] data = new byte[fakeErrorResponses.Count() - 1];
            _RandomNumberGenerator.GetBytes(data);

            int ErrorIndex = data.Where(b => b < (255 / 2)).Count();

            return fakeErrorResponses[ErrorIndex];
        }

    }
}
