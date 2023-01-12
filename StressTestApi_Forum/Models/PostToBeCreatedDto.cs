using StressTestApi_Forum.Services.Efcore.Entities;

namespace StressTestApi_Forum.Models
{
    public class PostToBeCreatedDto
    {
        public string Title { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;
    }
}
