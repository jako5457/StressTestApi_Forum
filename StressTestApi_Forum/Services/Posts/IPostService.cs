using StressTestApi_Forum.Services.Efcore.Entities;

namespace StressTestApi_Forum.Services.Posts
{
    public interface IPostService
    {
        Task<Post> CreatePostAsync(Post post);
        Task<Post?> GetPostAsync(Guid PostId);
        Task<IEnumerable<Post>> GetPostsAsync(PostFilterSettings FilterSettings);
        Task<Post> RemovePostAsync(Guid PostId);
    }
}