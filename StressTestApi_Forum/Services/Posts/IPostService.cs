using StressTestApi_Forum.Models;
using StressTestApi_Forum.Services.Efcore.Entities;

namespace StressTestApi_Forum.Services.Posts
{
    public interface IPostService
    {
        Task<PostDto> CreatePostAsync(Post post);
        Task<PostDto?> GetPostAsync(Guid PostId);
        Task<IEnumerable<PostDto>> GetPostsAsync(PostFilterSettings FilterSettings);
        Task RemovePostAsync(Guid PostId);
    }
}