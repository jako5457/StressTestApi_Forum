using Microsoft.Extensions.Options;
using RobotsTxt;
using System.Text;

namespace StressTestApi_Forum
{
    public class RobotTxtProvider : IRobotsTxtProvider
    {
        private readonly IConfiguration _Configuration;
        private readonly ILogger<RobotTxtProvider> _Logger;
        private readonly IHostEnvironment _Environment;

        public RobotTxtProvider(IConfiguration configuration,ILogger<RobotTxtProvider> logger,IHostEnvironment environment)
        {
            _Configuration = configuration;
            _Logger = logger;
            _Environment = environment;
        }

        public async Task<RobotsTxtResult> GetResultAsync(CancellationToken cancellationToken)
        {
            _Logger.LogInformation("A bot has requested robots.txt");

            var builder = new RobotsTxtOptionsBuilder();

            if (_Environment.IsDevelopment())
            {
                builder = builder.AddSection(options => options
                                                            .AddComment("This is a development build.")
                                                            .AddUserAgent("*")
                                                            .Allow("/"));
            }
            else
            {
                builder = builder
                    .AddSection(options => options
                                    .AddComment("Allow google to crawl the website.")
                                    .AddUserAgent("Googlebot")
                                    .Allow("/")
                )
                    .DenyAll();
            }

            var options = builder.Build();
            var content = options.ToString();
            var buffer = Encoding.UTF8.GetBytes(content).AsMemory();
            return new RobotsTxtResult(buffer,10000);
        }
    }
}
