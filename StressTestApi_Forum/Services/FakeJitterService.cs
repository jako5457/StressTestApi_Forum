using System.Security.Cryptography;

namespace StressTestApi_Forum.Services
{
    public class FakeJitterService : IFakeJitterService
    {
        //Occurrance of errors (Higher = More errors)
        const int occourance = 9;

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

        public int RandomDelay { 
            get {
                byte[] data = new byte[20];

                _RandomNumberGenerator.GetBytes(data);

                return data.Select(b => Convert.ToInt32(b)).Sum();
            } 
        }

        public bool HasError()
        {
            byte[] data = new byte[100];
            _RandomNumberGenerator.GetBytes(data);

            int count = data.Where(b => b < (255 / (occourance - 1))).Count();

            return count < (100 / occourance - 2);
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
