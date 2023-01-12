using Microsoft.Extensions.Options;
using RobotsTxt;
using StressTestApi_Forum.Configurations;
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

            RobotConfiguration OverrideConfig = new();
            _Configuration.Bind("RobotOverride",OverrideConfig);

            var builder = new RobotsTxtOptionsBuilder();

            if (!OverrideConfig.Enabled)
            {
                if (_Environment.IsDevelopment())
                {
                    builder = builder.AddSection(options => options
                                              .AddComment("This is a development build.")
                                              .AddUserAgent("*")
                                              .Allow("/"));
                }
                else
                {
                    builder = builder.AddSection(options => options
                                        .AddComment("Allow google to crawl the webapi")
                                        .AddUserAgent("Googlebot")
                                        .Allow("/")
                                     )
                                     .AddSection(options => options
                                        .AddComment("Allow Duckduckgo to crawl the webapi")
                                        .AddUserAgent("duckduckbot")
                                        .Allow("/")
                                     )
                                     .DenyAll();
                }
            }
            else
            {
                if (OverrideConfig.AllowAll)
                {
                    builder = builder.AddSection(options => options
                                             .AddComment("Overrided")
                                             .AddUserAgent("*")
                                             .Allow("/"));
                }
                else
                {
                    builder = builder.AddSection(options => options
                                            .AddComment("Overrided")
                                            .AddUserAgent("*")
                                            .Disallow("/"));
                }
            }     

            var options = builder.Build();
            var content = options.ToString();
            var buffer = Encoding.UTF8.GetBytes(content).AsMemory();
            return new RobotsTxtResult(buffer,1000);
        }
    }
}
