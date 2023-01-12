using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StressTestApi_Forum.Services.Efcore;
using StressTestApi_Forum.Services.Efcore.Entities;
using System.Reflection.Metadata.Ecma335;

namespace StressTestApi_Forum.Services.Users
{
    public class UserService : IUserService
    {

        private readonly ForumContext _ForumContext;

        public UserService(ForumContext forumContext)
        {
            _ForumContext = forumContext;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var query = _ForumContext.Users;

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetUserAsync(Guid UserId)
        {
            return await _ForumContext.Users.Where(u => u.UserId == UserId).FirstOrDefaultAsync();
        }

        public async Task<User> AddUserAsync(User user)
        {
            user.UserId = Guid.NewGuid();

            _ForumContext.Users.Add(user);

            await _ForumContext.SaveChangesAsync();

            return user;
        }
    }
}
