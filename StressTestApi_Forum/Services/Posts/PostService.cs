using Microsoft.EntityFrameworkCore;
using StressTestApi_Forum.Models;
using StressTestApi_Forum.Services.Efcore;
using StressTestApi_Forum.Services.Efcore.Entities;
using System.Security.Cryptography.X509Certificates;

namespace StressTestApi_Forum.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly ForumContext _ForumContext;

        public PostService(ForumContext forumContext)
        {
            _ForumContext = forumContext;
        }


        public async Task<IEnumerable<PostDto>> GetPostsAsync(PostFilterSettings FilterSettings)
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

            return await query.Include(p => p.Author).Select(p => p.ToPostDto()).AsNoTracking().ToListAsync();
        }

        public async Task<PostDto?> GetPostAsync(Guid PostId)
        {
            return await _ForumContext.Posts.Where(p => p.PostId == PostId).Select(p => p.ToPostDto()).FirstOrDefaultAsync();
        }

        public async Task<PostDto> CreatePostAsync(Post post)
        {
            post.PostId = Guid.NewGuid();

            _ForumContext.Posts.Add(post);

            await _ForumContext.SaveChangesAsync();

            await _ForumContext.Entry(post).Navigation(nameof(post.Author)).LoadAsync();

            return post.ToPostDto();
        }

        public async Task RemovePostAsync(Guid PostId)
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
        }
    }
}
