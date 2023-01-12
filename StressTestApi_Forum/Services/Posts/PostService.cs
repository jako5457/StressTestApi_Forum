using Microsoft.EntityFrameworkCore;
using StressTestApi_Forum.Services.Efcore;
using StressTestApi_Forum.Services.Efcore.Entities;
using System.Security.Cryptography.X509Certificates;

namespace StressTestApi_Forum.Services.Posts
{
    public class PostService
    {
        private readonly ForumContext _ForumContext;

        public PostService(ForumContext forumContext)
        {
            _ForumContext = forumContext;
        }


        public async Task<IEnumerable<Post>> GetPostsAsync(PostFilterSettings FilterSettings)
        {
            var query = _ForumContext.Posts.AsQueryable();

            if (FilterSettings.category != 0)
            {
                query = query.Where(p => p.CategoryId == FilterSettings.category);
            }

            if (!string.IsNullOrWhiteSpace(FilterSettings.query))
            {
                query.Where(p => p.Title.Contains(FilterSettings.query) || p.Body.Contains(FilterSettings.query));
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<Post?> GetPostAsync(Guid PostId)
        {
            return await _ForumContext.Posts.Where(p => p.PostId== PostId).FirstOrDefaultAsync();
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            post.PostId = Guid.NewGuid();

            _ForumContext.Posts.Add(post);

            await _ForumContext.SaveChangesAsync();

            return post;
        }

        public async Task<Post> RemovePostAsync(Guid PostId)
        {
            var post = await _ForumContext.Posts.Where(p => p.PostId == PostId).FirstOrDefaultAsync();

            if (post != null)
            {
                _ForumContext.Posts.Remove(post);
                await _ForumContext.SaveChangesAsync();
            }
            else
            {
               throw new Exception("User not found");
            }

            return post;
        }
    }
}
