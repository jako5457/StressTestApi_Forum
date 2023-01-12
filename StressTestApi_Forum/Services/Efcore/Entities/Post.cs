namespace StressTestApi_Forum.Services.Efcore.Entities
{
    public class Post
    {

        public Guid PostId { get; set; } 

        public string Title { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public Guid Auhtorid { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; } = default!;

        public User Author { get; set; } = default!;

    }
}
