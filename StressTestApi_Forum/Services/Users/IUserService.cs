using StressTestApi_Forum.Services.Efcore.Entities;

namespace StressTestApi_Forum.Services.Users
{
    public interface IUserService
    {
        Task<User> AddUserAsync(User user);
        Task<User?> GetUserAsync(Guid UserId);
        Task<IEnumerable<User>> GetUsersAsync();
    }
}