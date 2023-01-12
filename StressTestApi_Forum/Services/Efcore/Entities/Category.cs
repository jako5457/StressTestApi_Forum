namespace StressTestApi_Forum.Services.Efcore.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; } = string.Empty;

        public IEnumerable<Post> Posts { get; set; } = Enumerable.Empty<Post>();
    }
}
