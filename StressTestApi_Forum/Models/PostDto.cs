namespace StressTestApi_Forum.Models
{
    public class PostDto
    {
        public Guid PostId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string AuthorId { get; set; } = string.Empty;
    }
}
