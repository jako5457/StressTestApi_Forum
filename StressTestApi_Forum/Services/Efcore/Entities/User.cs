namespace StressTestApi_Forum.Services.Efcore.Entities
{
    public class User
    {
        public Guid UserId { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
