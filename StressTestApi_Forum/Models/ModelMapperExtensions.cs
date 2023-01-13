using StressTestApi_Forum.Services.Efcore.Entities;

namespace StressTestApi_Forum.Models
{
    public static class ModelMapperExtensions
    {

        public static PostDto ToPostDto(this Post post)
        {
            return new PostDto
            {
                PostId = post.PostId,
                Title = post.Title,
                Body = post.Body,
                Author = post.Author.Name,
                AuthorId = post.Author.UserId.ToString(),
            };
        }

    }
}
