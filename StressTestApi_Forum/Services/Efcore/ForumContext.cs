using Microsoft.EntityFrameworkCore;
using StressTestApi_Forum.Services.Efcore.Entities;
using System.Security.Cryptography.X509Certificates;

namespace StressTestApi_Forum.Services.Efcore
{
    public class ForumContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                            .HasMany<Post>()
                            .WithOne(p => p.Author)
                            .HasForeignKey(p => p.Auhtorid);

            modelBuilder.Entity<Category>()
                            .HasMany(c => c.Posts)
                            .WithOne(p => p.Category)
                            .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<User>()
                .HasData(
                    new User()
                    {
                        UserId = Guid.Parse("94710243-279a-41c3-aedb-9ea86a4f2433"),
                        Name = "User1"
                    },
                    new User()
                    {
                        UserId = Guid.Parse("0ae91e75-4d83-4ecb-808f-edd58693d84e"),
                        Name = "User2"
                    },
                    new User()
                    {
                        UserId = Guid.Parse("b51ce5fe-e2cb-46d1-96f6-33d49b9ef5b0"),
                        Name = "User3"
                    });

            modelBuilder.Entity<Category>()
                .HasData(new Category()
                {
                    CategoryId = 1,
                    Name = "Main"
                });

            base.OnModelCreating(modelBuilder);
        }

    }
}
